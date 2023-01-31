using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth.Interfaces
{
    public interface IPageAssignService
    {
        Task<int> SaveUserAccessPage(UserAccessPage userAccessPage);

        Task<IEnumerable<NavbarViewModel>> GetNavbarDataByUser(string userName, string lang);


        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserType(string userTypeId);
		Task<IEnumerable<NavbarWithRoleVm>> GetAllNavbars(string roleId);
        Task<IEnumerable<Navbars>> GetNavbarsByUserRoleId(string roleId);
        Task<IEnumerable<Module>> GetAllModulesByRoleId(string roleId);
		Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeId);
        Task<IEnumerable<Navbars>> GetNavbars(string userName);
        Task<IEnumerable<Module>> GetERPModules();
        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeModule(string userTypeId, string userTypeIdM);
        
        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByModule(string userTypeId, int ModuleId);
        Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByMenu(string userTypeId, int ModuleId);
        Task<IEnumerable<string>> GetUserMenuAccessByRoles(string[] roles);
        

    }
}
