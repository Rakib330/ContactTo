using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth.Interfaces
{
    public interface IModuleAssignService
    {
        Task<int> SaveModuleAccessPage(ModuleAccessPage userAccessPage);
        Task<IEnumerable<ModuleAccessPage>> GetModuleAccessPages();
        Task<IEnumerable<Module>> GetERPModules();
        Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid);
        Task<IEnumerable<ModuleAccessPageViewModel>> GetModuleAccessPagesactive(string UserTypeId);
        Task<IEnumerable<Module>> GetERPModulesForTeam();




    }
}
