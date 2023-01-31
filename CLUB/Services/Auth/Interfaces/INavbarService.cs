using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth.Interfaces
{
    public interface INavbarService
    {
        Task<bool> DeleteNavbarItemById(int id);
        Task<IEnumerable<Navbars>> GetNavbarItem();
        Task<IEnumerable<Navbars>> GetNavigationMenu(string userName);
        Task<IEnumerable<Navbars>> GetNavbarItemByParent();
        Task<IEnumerable<Navbars>> GetNavbarItemByParentByModule(int moduleId, int isParent);
        Task<Navbars> GetNavbarItemById(int id);
        Task<bool> SaveNavbarItem(Navbars navbar);
        Task<IEnumerable<Navbars>> GetNavbarItemByParentIdByModule(int moduleId, int isParent);
        Task<int> GetNavbarParentIdById(int id);
        Task<IEnumerable<Navbars>> GetNavbarItemByModule(int id);
        Task<bool> SaveModule(Module module);
        Task<IEnumerable<Module>> GetModules();
        //Task<bool> SaveMasterMenu(MasterMenu masterMenu);
        //Task<IEnumerable<MasterMenu>> GetMasterMenus();
        Task<Navbars> GetNavByAreaControllerAction(string area, string controller, string action);
    }
}
