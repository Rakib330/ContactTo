
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Navbar.Interface
{
    public interface IModuleAssaign
    {
        Task<IEnumerable<Module>> GetERPModules();
        Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid);
        Task<int> SaveModuleAccessPage(ModuleAccessPage userAccessPage);
    }
}
