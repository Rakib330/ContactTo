using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth
{
    public class NavbarServices : INavbarService
    {
        private readonly ApplicationDbContext _context;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public NavbarServices(ApplicationDbContext context, 
            //RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            //_roleManager = roleManager;
            _userManager = userManager;
        }

        #region Navbar
        public async Task<bool> DeleteNavbarItemById(int id)
        {
            _context.navbars.Remove(_context.navbars.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavbarItem()
        {
            var result = await (from n in _context.navbars
                                join m in _context.modules on n.moduleId equals m.Id
                                join pn in _context.navbars on n.parentID equals pn.Id into pon
                                from pnn in pon.DefaultIfEmpty()
                                where n.status == true
                                select new Navbars
                                {
                                    Id = n.Id,
                                    nameOptionBangla = n.nameOptionBangla,
                                    nameOption = n.nameOption,
                                    area = n.area,
                                    controller = n.controller,
                                    action = n.action,
                                    imageClass = n.imageClass,
                                    activeLi = n.activeLi,
                                    status = n.status,
                                    parentID = n.parentID,
                                    isParent = n.isParent,
                                    displayOrder = n.displayOrder,
                                    moduleId = n.moduleId,
                                    module = m,
                                    parentName = pnn.nameOption
                                }).ToListAsync();
            return result;
            //return await _context.Navbars.Include(x=>x.module).Where(x=>x.status == true).AsNoTracking().ToListAsync();
        }
        public async Task<bool> SaveModule(Module module)
        {
            if (module.Id != 0)
                _context.modules.Update(module);
            else
                _context.modules.Add(module);

            return 1 == await _context.SaveChangesAsync();
        }
        //public async Task<bool> SaveMasterMenu(MasterMenu masterMenu)
        //{
        //    if (masterMenu.Id != 0)
        //        _context.MasterMenus.Update(masterMenu);
        //    else
        //        _context.MasterMenus.Add(masterMenu);

        //    return 1 == await _context.SaveChangesAsync();
        //}
        public async Task<IEnumerable<Module>> GetModules()
        {
            return await _context.modules.OrderBy(x => x.shortOrder).ToListAsync();
        }
        //public async Task<IEnumerable<MasterMenu>> GetMasterMenus()
        //{
        //    return await _context.MasterMenus.OrderBy(x => x.areaName).ToListAsync();
        //}


        public async Task<IEnumerable<Navbars>> GetNavbarItemByRole(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            //var records=from n in _context.Navbars
            //            join b in _context.Navbars on n.parentID equals b.Id
            //            join c in _context.Navbars on b.parentID equals c.Id
            //            join m in _context.ERPModules on c.moduleId equals m.Id
            //            join u in _context.UserAccessPages on n.Id equals u.navbarId
            //            where u.applicationRoleId.Contains(roles.)

            var result = await (from n in _context.navbars
                                join m in _context.modules on n.moduleId equals m.Id
                                join pn in _context.navbars on n.parentID equals pn.Id into pon
                                from pnn in pon.DefaultIfEmpty()
                                where n.status == true
                                select new Navbars
                                {
                                    Id = n.Id,
                                    nameOptionBangla = n.nameOptionBangla,
                                    nameOption = n.nameOption,
                                    area = n.area,
                                    controller = n.controller,
                                    action = n.action,
                                    imageClass = n.imageClass,
                                    activeLi = n.activeLi,
                                    status = n.status,
                                    parentID = n.parentID,
                                    isParent = n.isParent,
                                    displayOrder = n.displayOrder,
                                    moduleId = n.moduleId,
                                    module = m,
                                    parentName = pnn.nameOption
                                }).ToListAsync();
            return result;

        }

        public async Task<IEnumerable<Navbars>> GetNavbarItemByParent()
        {
            return await _context.navbars.Where(x => x.isParent == 1 && x.status == true).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavbarItemByModule(int id)
        {
            return await _context.navbars.Where(x => x.isParent == 0 && x.status == true && x.moduleId == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavbarItemByParentByModule(int moduleId, int isParent)
        {
            return await _context.navbars.Where(x => x.status == true && x.moduleId == moduleId && x.isParent == isParent).AsNoTracking().ToListAsync();
        }

        public async Task<Navbars> GetNavbarItemById(int id)
        {
            return await _context.navbars.FindAsync(id);
        }

        public async Task<bool> SaveNavbarItem(Navbars navbar)
        {
            if (navbar.Id != 0)
                _context.navbars.Update(navbar);
            else
                _context.navbars.Add(navbar);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavigationMenu(string userName)
        {
            var result = await (from n in _context.navbars
                                join em in _context.modules on n.moduleId equals em.Id
                                join uap in _context.userAccessPages on n.Id equals uap.navbarId
                                join ar in _context.Roles on uap.applicationRoleId equals ar.Id
                                join aur in _context.UserRoles on ar.Id equals aur.RoleId
                                join au in _context.Users on aur.UserId equals au.Id
                                where au.UserName == (userName == "suza" ? au.UserName : userName)
                                select new Navbars
                                {
                                    Id = n.Id,
                                    moduleName = em.moduleName,
                                    nameOption = n.nameOption,
                                    area = n.area,
                                    controller = n.controller,
                                    action = n.action,
                                    status = n.status,
                                    parentID = n.parentID,
                                    isParent = n.isParent,
                                    moduleId = n.moduleId,
                                    displayOrder = n.displayOrder
                                }).AsNoTracking().ToListAsync();
            return result;

        }
        public async Task<IEnumerable<Navbars>> GetNavbarItemByParentIdByModule(int moduleId, int isParent)
        {
            return await _context.navbars.Where(x => x.status == true && x.moduleId == moduleId && x.parentID == isParent).AsNoTracking().ToListAsync();
        }
        public async Task<int> GetNavbarParentIdById(int id)
        {
            var nab = await _context.navbars.FindAsync(id);
            return nab.parentID;
        }


        public async Task<Navbars> GetNavByAreaControllerAction(string area, string controller, string action)
        {
            return await _context.navbars.Where(x => x.area == area && x.controller == controller && x.action == action).FirstOrDefaultAsync();
        }
        #endregion
    }
}
