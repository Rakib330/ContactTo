using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.Employee.Models;
using CLUB.Data;
using CLUB.Data.Entity.Employee;
using CLUB.Services.Employee.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.Employee
{
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly ApplicationDbContext _context;

        public PersonalInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        //EmployeeInfo
        public async Task<bool> DeleteEmployeeInfoById(int id)
        {
            _context.employeeInfos.Remove(_context.employeeInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetEmployeeInfoCheck(string empCode, int id)
        {
            var Result = await _context.employeeInfos.Where(x => x.employeeCode == empCode && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfo()
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.employeeInfos.ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoWithUser()
        {
            return await _context.employeeInfos.Where(x=>x.ApplicationUserId != null).Include(x=>x.ApplicationUser).AsNoTracking().ToListAsync();
        }
        

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByType()
        {
            return await _context.employeeInfos.Where(x=>x.employeeTypeId == 1).AsNoTracking().ToListAsync();
        }

        


        

        public async Task<EmployeeInfo> GetEmployeeInfoById(int? id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await _context.employeeInfos.Include(x => x.religion).Include(x => x.employeeType).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveEmployeeInfo(EmployeeInfo employeeInfo)
        {
            if (employeeInfo.Id != 0)
                _context.employeeInfos.Update(employeeInfo);
            else
                _context.employeeInfos.Add(employeeInfo);
            await _context.SaveChangesAsync();
            return employeeInfo.Id;
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByCode(string code)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId)
        {
            return await _context.employeeWithDesignations.FromSql($"sp_GetEmployeeDetailsList @p0,@p1", empId, string.Empty).ToListAsync();
        }

        public async Task<string> GetEmployeeNameCodeById(int Id)
        {
            EmployeeInfo data = await _context.employeeInfos.FindAsync(Id);
            return data?.nameEnglish + "-" + data?.employeeCode;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("&");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "EmpType")
                    {
                        queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    }
                    else if (SepToken[0] == "PRL")
                    {
                        DateTime nowAndEx = DateTime.Now.AddMonths(Int32.Parse(SepToken[1]));
                        DateTime now = DateTime.Now;
                        queryData = queryData.Where(x => (DateTime.Parse(x.LPRDate) <= nowAndEx && DateTime.Parse(x.LPRDate) >= now));
                    }
                }
            }
            #endregion

            #region Result Process
            IEnumerable<EmployeeInfo> data = await queryData.ToListAsync();
            List<AllEmployeeJson> filteredData = new List<AllEmployeeJson>();

            foreach (EmployeeInfo employeeInfo in data)
            {
                filteredData.Add(new AllEmployeeJson
                {
                    employeeCode = employeeInfo.employeeCode,
                    nameEnglish = employeeInfo.nameEnglish,
                    emailAddress = employeeInfo.emailAddress,
                    mobileNumberOffice = employeeInfo.mobileNumberOffice,
                    designation = employeeInfo.gpfAcNo,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' href='/Employee/Info/EditGrid/{employeeInfo.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' href='/Employee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/Employee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }

        //Here We GetQuery Result
        public async Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleMore(string queryString, string org)
        {
            IQueryable<EmployeeInfo> queryData = _context.employeeInfos.Where(x => x.orgType == org);

            #region Filtering...

            string[] Tokens = queryString.Split("|");
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken[0] == "Gender") queryData = queryData.Where(x => x.gender == SepToken[1]);
                    else if (SepToken[0] == "MaritalStatus") queryData = queryData.Where(x => x.maritalStatus == SepToken[1]);
                    else if (SepToken[0] == "Religion") queryData = queryData.Where(x => x.religionId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "BloodGroup") queryData = queryData.Where(x => x.bloodGroup == SepToken[1]);
                    else if (SepToken[0] == "MemberType") queryData = queryData.Where(x => x.employeeTypeId == Int32.Parse(SepToken[1]));
                    else if (SepToken[0] == "FreedomFighter") queryData = queryData.Where(x => x.freedomFighter == (SepToken[1] == "Yes" ? true : false));
                    else if (SepToken[0] == "Division")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.divisionId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "District")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.districtId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Thana")
                    {
                        List<int> Ids = await _context.addresses.Where(x => x.thanaId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Degree")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.degreeId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Group")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.reldegreesubjectId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    else if (SepToken[0] == "Institution")
                    {
                        List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                    //else if (SepToken[0] == "SpacialSkill")
                    //{
                    //    List<int> Ids = await _context.educationalQualifications.Where(x => x.organizationId == Int32.Parse(SepToken[1])).Select(x => x.employeeId).ToListAsync();
                    //    queryData = queryData.Where(x => Ids.Contains(x.Id));
                    //}

                    else if (SepToken[0] == "dateOfBirth") queryData = queryData.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2])));
                    
                    else if (SepToken[0] == "dateOfBirthSpouse")
                    {
                        List<int> Ids = await _context.spouses.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfMarriage")
                    {
                        List<int> Ids = await _context.spouses.Where(x => (x.dateOfMarriage >= DateTime.Parse(SepToken[1]) && x.dateOfMarriage <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }

                    else if (SepToken[0] == "dateOfBirthChild")
                    {
                        List<int> Ids = await _context.childrens.Where(x => (x.dateOfBirth >= DateTime.Parse(SepToken[1]) && x.dateOfBirth <= DateTime.Parse(SepToken[2]))).Select(x => x.employeeId).ToListAsync();
                        queryData = queryData.Where(x => Ids.Contains(x.Id));
                    }
                }
            }
            #endregion

            return await queryData.Select(x => new AllEmployeeJson
            {
                employeeCode = x.employeeCode,
                nameEnglish = x.nameEnglish,
                designation = x.designation,
                emailAddress = x.emailAddress,
                mobileNumberOffice = x.mobileNumberPersonal,
                action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' href='/Employee/Info/Index/{x.Id}'><i class='fa fa-edit'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' href='/Employee/InfoView/Index/{x.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/Employee/InfoView/pdspdf/{x.Id}'><i class='fa fa-print'></i></a>"
            }).ToListAsync();
        }

        public async Task<EmployeeInfo> GetFreeEmployeeByCode(string code)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code && x.ApplicationUserId == null).FirstOrDefaultAsync();
        }
        public async Task<EmployeeInfo> GetExistEmployeeByCode(string code)
        {
            var data = await _context.Users.Where(x => x.UserName == code).FirstOrDefaultAsync();
            if (data != null)
            {
                return await _context.employeeInfos.Where(x => x.employeeCode == data.UserName).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }

            //return await _context.employeeInfos.Where(x => x.employeeCode == code && x.ApplicationUserId != null).FirstOrDefaultAsync();

        }

        public async Task<bool> UpdateEmployee(int Id, string authId, string org)
        {
            EmployeeInfo data = await _context.employeeInfos.FindAsync(Id);

            if (data == null) return false;
            data.ApplicationUserId = authId;
            data.orgType = org;

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByOrg(string org)
        {
            return await _context.employeeInfos.Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code, string orgType)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == code).Where(x => x.orgType == orgType).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<string> GetAuthCodeByUserId(int empId)
        {
            return await _context.employeeInfos.Where(x => x.Id == empId).AsNoTracking().Select(x => x.ApplicationUserId).FirstOrDefaultAsync();
        }

        public async Task<int> IsThisEmpIDPresent(string employeeId)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == employeeId).AsNoTracking().Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<string> GetEmployeeIDByAuthID(string empId)
        {
            return await _context.employeeInfos.Where(x => x.ApplicationUserId == empId).Select(x => x.Id.ToString()).FirstOrDefaultAsync();
        }
        public async Task<int> GetEmployeeIDByAuthIDint(string empId)
        {
            return await _context.employeeInfos.Where(x => x.employeeCode == empId).Select(x => x.Id).FirstOrDefaultAsync();
        }
    }
}
