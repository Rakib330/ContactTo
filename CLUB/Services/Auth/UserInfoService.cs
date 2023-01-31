using CLUB.Areas.Auth.Models;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Employee;
using CLUB.Data.Entity.Navbar;
using CLUB.Models.Auth;
using CLUB.Services.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth
{
    public class UserInfoService : IUserInfos
    {
        private readonly ApplicationDbContext _context;
        public IConfiguration _config { get; }
        public UserInfoService(ApplicationDbContext context,IConfiguration Configuration)
        {
            _context = context;
            _config = Configuration;
        }

        public async Task<ApplicationUser> GetUserDetailsByUserName(string username)
        {
            return await _context.Users.Where(x => x.UserName == username).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<EmployeeInfo> GetEmployeeByUserName(string username)
        {
            var user = await _context.Users.Where(x => x.UserName == username).AsNoTracking().FirstOrDefaultAsync();
            return await _context.employeeInfos.Where(x => x.ApplicationUserId == user.Id).AsNoTracking().FirstOrDefaultAsync();
        }
        
       
        //public async Task<int> GetMaxUserId()
        //{
        //    var result = await _context.Users.MaxAsync(x => x.Id);
        //    return result;
        //}

        public async Task<string> VerifyResetCode(string username)
        {
            var data = await _context.userOTPLogs.Where(x => x.username == username).LastOrDefaultAsync();
            return data.otp;
        }

        public async Task<AspNetUsersViewModel> GetUserInfoByUser(string userName)
        {
            var result = await _context.Users.Where(x => x.UserName == userName).AsNoTracking().Select(x => new AspNetUsersViewModel
            {
                aspnetId = x.Id,
                UserName = x.UserName,
                email = x.Email
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ApplicationUser> GetUserBasicInfoes(string userName)
        {
            var id = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            return id;

        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteUserRoleListByUserId(string Id)
        {
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == Id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<ApplicationUser> GetUserByUserName(string username)
        {
            var data = await _context.Users.Where(x => x.UserName == username).AsNoTracking().FirstOrDefaultAsync();
            return data;
        }
        public async Task<int> saveUser(ApplicationUser user)
        {
            try
            {
                if (user.Id != null)
                {
                    _context.Update(user);
                }
                else
                {
                    _context.Add(user);
                }
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<EmployeeInfo> GetEmployeeInfoByUsername(string username)
        {
            return await _context.employeeInfos.Where(x => x.ApplicationUser.UserName == username).FirstOrDefaultAsync();
        }
        public async Task<List<string>> GetRoleIdByUserId(string userid)
        {
            return await _context.UserRoles.Where(x => x.UserId == userid).Select(x => x.RoleId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfo()
        {
            var result = (from U in _context.Users
                          join E in _context.employeeInfos on U.Id equals E.ApplicationUserId
                          select new AspNetUsersViewModel
                          {
                              aspnetId = U.Id,
                              UserName = U.UserName,
                              Email = U.Email,
                              //EmpCode = U.empCode,
                              // FinancialValue = U.MaxAmount,
                              EmpName = E.nameEnglish,
                              EmployeeId = E.Id,


                          }).ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<string>> GetRoleListByUserId(string Id)
        {
            return await _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToListAsync();
        }
        public async Task<bool> DeleteRoleById(string Id)
        {
            _context.Roles.Remove(_context.Roles.Where(x => x.Id == Id).First());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<GetAllUserViewModel>> GetAllUserInfos()
        {
            var result = await _context.getAllUserViewModels.FromSql($"sp_GetAllUserInfos").ToListAsync();
            return result;
        }

        public async Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosWithRegigonArea(string rolename)
        {
            try
            {
                var data = await _context.getAllUserViewNewModels.FromSql($"sp_GetAllUserInfosWithRegigonArea {rolename}").ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        // public async Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosForProxyUser(string userName,string rolename, string branchId)
        //{
        //    try
        //    {
        //        var data= await _context.getAllUserViewNewModels.FromSql($"sp_GetAllUserInfosForProxyUser {userName},{rolename},{branchId}").ToListAsync();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }


        //}



        //public async Task<ClearDataVm> ClearAllData()
        //{
        //    var result = await _context.clearDataVms.FromSql($"sp_ClearAllData").FirstOrDefaultAsync();
        //    return result;
        //}
        //public async Task<IEnumerable<ApplicationRoleViewModel>> GetAllRoles()
        //{
        //    var data = await _context.Roles.Where(x => x.Name != "admin").Select(x => new ApplicationRoleViewModel
        //    {
        //        RoleId = x.Id,
        //        RoleName = x.Name
        //    }).ToListAsync();
        //    return data;
        //}
        //public async Task<IEnumerable<ApplicationRole>> GetRolesByUserId(string id)
        //{
        //    var data = await (from ur in _context.UserRoles.Where(x => x.UserId == id)
        //                      join r in _context.Roles
        //                      on ur.RoleId equals r.Id
        //                      select r).ToListAsync();
        //    return data;
        //}

        public async Task<List<string>> GetRoleNaturesByUserName(string username)
        {
            var data = await (from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where u.UserName == username
                        select r.Name).ToListAsync();
            return data;
        }

        public async Task<int> SaveUserOTPLog(UserOTPLog model)
        {
            if (model.Id != 0)
            {
                _context.userOTPLogs.Update(model);
            }
            else
            {
                _context.userOTPLogs.Add(model);
            }

            return await _context.SaveChangesAsync();
        }
        public async Task<ApplicationUser> GetAspnetuserByUser(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<int> saveUserLogHistory(UserLogHistory model)
        {
            if (model.Id != 0)
            {
                _context.userLogHistories.Update(model);
            }
            else
            {
                _context.userLogHistories.Add(model);
            }
            return await _context.SaveChangesAsync();
        }
        public async Task<string> GetUserIdByUserName(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).Select(x => x.Id).AsNoTracking().FirstOrDefaultAsync();
        }

        //public async Task<ApplicationRole> GetRoleByRoleName(string rolename)
        //{
        //    return await _context.Roles.Where(x => x.Name == rolename).AsNoTracking().FirstOrDefaultAsync();
        //}
        public async Task<string> GetRoleByUserId(string userId)
        {
            return await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<string> GetRoleNameByRoleId(string roleId)
        {
            return await _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<string> GetRoleNatureByUserId(string userName)
        {
            var data = await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where u.UserName == userName
                              select r.Name).FirstOrDefaultAsync();

            return Convert.ToString(data);
        }
        public async Task<string> GetRoleNameByUserId(string userName)
        {
            var data = await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where u.UserName == userName
                              select r.Name).FirstOrDefaultAsync();

            return Convert.ToString(data);
        }
        public async Task<string> GetEmployeeIdByUserId(string userId)
        {
            var data = await _context.employeeInfos.Where(x => x.ApplicationUserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
            //return data.ToString();
            return Convert.ToString(data);
        }
        
        public int CheckUserRole(string username, string roleName)
        {
            var data = (from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where u.UserName == username && r.Name == roleName
                        select r).Count();
            return data;
        }

        public int CheckDuplicateUser(string UserName)
        {
            var data = _context.Users.Where(x => x.UserName == UserName).AsNoTracking().Count();
            return data;
        }

        public async Task<IEnumerable<ApplicationRoleViewModel>> GetAllUserRoles()
        {
            var data = await _context.Roles.Select(x => new ApplicationRoleViewModel
            {
                RoleId = x.Id,
                RoleName = x.Name

            }).ToListAsync();
            return data;
        }


        public async Task<NavbarViewModel> AssignPage(string userName)
        {
            var userId = _context.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();

            List<string> roleids = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            string LoginedRoleid = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefaultAsync();
            List<int?> lstmodule = _context.userAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

            List<int> lstparentId = _context.navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();
            List<int> lstparentIdF = _context.navbars.Where(x => lstparentId.Contains(x.Id)).Select(x => x.parentID).ToList();

            IEnumerable<Navbars> navdata = await _context.navbars.Include(x => x.module).Where(x => x.status == true).OrderBy(x => x.displayOrder).ToListAsync();

            var adminrole = _context.UserRoles.Where(x => x.UserId == userId).ToList();
            if (adminrole.Count() == 0)
            {
                navdata = navdata.Where(x => lstmodule.Contains(x.Id) || lstparentId.Contains(x.Id) || lstparentIdF.Contains(x.Id));
            }
            List<int?> modid = navdata.Select(x => x.moduleId).ToList();
            IEnumerable<int?> AssindModulIds = await _context.moduleAccessPages.Where(x => x.applicationRoleId == LoginedRoleid).Select(x=>x.eRPModuleId).ToListAsync();

            //IEnumerable<Module> modules = await _context.modules.OrderBy(x => x.shortOrder).ToListAsync();
            List<Module> modules = new List<Module>();
            foreach (var RId in AssindModulIds)
            {
                modules.Add(new Module
                {
                    Id = await _context.modules.Where(x => x.Id == RId).Select(x => x.Id).FirstOrDefaultAsync(),
                    moduleName = await _context.modules.Where(x => x.Id == RId).Select(x=>x.moduleName).FirstOrDefaultAsync(),
                });
            }
            //if (adminrole.Count() == 0)
            //{
            //    modules = modules.Where(x => modid.Contains(x.Id));
            //}
            NavbarViewModel model = new NavbarViewModel
            {
                navbars = navdata,//await pageAssignService.GetNavbars(userName),
                ERPModules = modules,//await pageAssignService.GetERPModules(),
                userRoles = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync()
            };


            return model;
        }

    }
}
