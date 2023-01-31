
using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Employee;
using CLUB.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CLUB.Services.Auth.Interfaces
{
    public interface IUserInfos
    {
        Task<ApplicationUser> GetUserDetailsByUserName(string username);
        //Task<int> GetMaxUserId();
		Task<string> VerifyResetCode(string username);
		Task<AspNetUsersViewModel> GetUserInfoByUser(string userName);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<bool> DeleteUserRoleListByUserId(string Id);
        Task<EmployeeInfo> GetEmployeeInfoByUsername(string username);
        Task<ApplicationUser> GetUserByUserName(string username);
        int CheckUserRole(string username, string roleName);
        Task<List<string>> GetRoleIdByUserId(string userid);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfo();
        Task<IEnumerable<string>> GetRoleListByUserId(string Id);
        Task<bool> DeleteRoleById(string Id);
        Task<IEnumerable<GetAllUserViewModel>> GetAllUserInfos();
        Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosWithRegigonArea(string rolename);
        //Task<IEnumerable<ApplicationRoleViewModel>> GetAllRoles();
        //Task<IEnumerable<ApplicationRole>> GetRolesByUserId(string id);
        Task<List<string>> GetRoleNaturesByUserName(string username);
        Task<int> SaveUserOTPLog(UserOTPLog model);
        Task<ApplicationUser> GetAspnetuserByUser(string userName);
		//Task<int> UpdateEmployeePosting(string code, string roleName, int? branchId, int? areaId, int? regionId, int? divisionId);
		Task<string> GetUserIdByUserName(string userName);
        //Task<ApplicationRole> GetRoleByRoleName(string rolename);
        Task<string> GetRoleByUserId(string userId);
        Task<string> GetRoleNameByRoleId(string roleId);
        Task<string> GetEmployeeIdByUserId(string userId);
        Task<EmployeeInfo> GetEmployeeByUserName(string username);
        Task<int> saveUser(ApplicationUser user);
        Task<int> saveUserLogHistory(UserLogHistory model);
        Task<string> GetRoleNatureByUserId(string userId);
        Task<string> GetRoleNameByUserId(string userId);
        int CheckDuplicateUser(string UserName);
        //Task<string> GetroleNameByAppId(string userId);
        Task<IEnumerable<ApplicationRoleViewModel>> GetAllUserRoles();
        Task<NavbarViewModel> AssignPage(string userName);
        //Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosForProxyUser(string userName, string rolename, string branchId);
        Task<ApplicationUser> GetUserBasicInfoes(string userName);
    }
}
