using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.Employee.Models;
using CLUB.Data.Entity.Employee;

namespace CLUB.Services.Employee.Interfaces
{
    public interface IPersonalInfoService
    {
        Task<int> SaveEmployeeInfo(EmployeeInfo employeeInfo);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfo();
        Task<EmployeeInfo> GetEmployeeInfoById(int? id);
        Task<bool> DeleteEmployeeInfoById(int id);
        Task<IEnumerable<EmployeeWithDesignationVM>> GetEmployeeInfoDetailsList(int empId);
        Task<EmployeeInfo> GetEmployeeInfoByCode(string code);
        Task<EmployeeInfo> GetEmployeeInfoByCodeAndOrg(string code,string orgType);
        Task<EmployeeInfo> GetFreeEmployeeByCode(string code);
        Task<EmployeeInfo> GetExistEmployeeByCode(string code);
        Task<string> GetEmployeeNameCodeById(int Id);
        Task<bool> UpdateEmployee(int Id, string authId, string org);
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByOrg(string org);
        Task<string> GetAuthCodeByUserId(int empId);

        Task<string> GetEmployeeIDByAuthID(string empId);
        Task<int> IsThisEmpIDPresent(string employeeId);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAble(string queryString, string org);
        Task<IEnumerable<AllEmployeeJson>> GetEmployeeInfoAsQueryAbleMore(string queryString, string org);

       
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoWithUser();
        Task<int> GetEmployeeIDByAuthIDint(string empId);
    }
}
