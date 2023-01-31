
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Navbar.Interface;

namespace CLUB.Services.Navbar
{
    public class ModuleAssaignService: IModuleAssaign
    {
        private readonly ApplicationDbContext _context;

        public ModuleAssaignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetERPModules()
        {
            return await _context.modules.ToListAsync();
        }
        public async Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid)
        {
            _context.moduleAccessPages.RemoveRange(_context.moduleAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<int> SaveModuleAccessPage(ModuleAccessPage userAccessPage)
        {
            try
            {
                if (userAccessPage.Id != 0)
                {
                    userAccessPage.updatedBy = userAccessPage.createdBy;
                    userAccessPage.updatedAt = DateTime.Now;
                    _context.moduleAccessPages.Update(userAccessPage);
                }
                else
                {
                    userAccessPage.createdAt = DateTime.Now;
                    _context.moduleAccessPages.Add(userAccessPage);
                }

                await _context.SaveChangesAsync();
                return userAccessPage.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
