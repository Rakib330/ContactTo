
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Navbar.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Navbar
{
    public class NavbarService: INavbar
    {
        private readonly ApplicationDbContext _context;

        public NavbarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _context.modules.ToListAsync();
        }
        public async Task<IEnumerable<Module>> GetModules()
        {
            return await _context.modules.OrderBy(x => x.shortOrder).ToListAsync();
        }
        public async Task<bool> SaveModule(Module module)
        {
            if (module.Id != 0)
                _context.modules.Update(module);
            else
                _context.modules.Add(module);

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Navbars>> GetNavbarItemByParent()
        {
            return await _context.navbars.Where(x => x.isParent == 1 && x.status == true).AsNoTracking().ToListAsync();
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
        }
        public async Task<bool> SaveNavbarItem(Navbars navbar)
        {
            if (navbar.Id != 0)
                _context.navbars.Update(navbar);
            else
                _context.navbars.Add(navbar);

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Navbars>> GetNavbarItemByModule(int id)
        {
            return await _context.navbars.Where(x => x.isParent == 0 && x.status == true && x.moduleId == id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<UserAccessPage>> GetAllUserAccessPages(string id)
        {
            return await _context.userAccessPages.Where(x => x.applicationRoleId == id).Include(x => x.navbar).Include(x => x.applicationRole).ToListAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavbarItemByParentByModule(int moduleId, int isParent)
        {
            return await _context.navbars.Where(x => x.status == true && x.moduleId == moduleId && x.isParent == isParent).AsNoTracking().ToListAsync();
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
    }
}
