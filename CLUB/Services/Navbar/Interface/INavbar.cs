
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Navbar.Interface
{
    public interface INavbar
    {
        Task<IEnumerable<Module>> GetAllModules();
        Task<IEnumerable<Module>> GetModules();
        Task<bool> SaveModule(Module module);
        Task<IEnumerable<Navbars>> GetNavbarItemByParent();
        Task<IEnumerable<Navbars>> GetNavbarItem();
        Task<bool> SaveNavbarItem(Navbars navbar);
        Task<IEnumerable<Navbars>> GetNavbarItemByModule(int id);
        Task<IEnumerable<UserAccessPage>> GetAllUserAccessPages(string id);

        Task<IEnumerable<Navbars>> GetNavbarItemByParentByModule(int moduleId, int isParent);
        Task<IEnumerable<Navbars>> GetNavbarItemByParentIdByModule(int moduleId, int isParent);
        Task<int> GetNavbarParentIdById(int id);
    }
}
