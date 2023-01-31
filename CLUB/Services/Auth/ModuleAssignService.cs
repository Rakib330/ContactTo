using CLUB.Areas.Auth.Models;
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth
{
    public class ModuleAssignService: IModuleAssignService
    {
        private readonly ApplicationDbContext _context;

        public ModuleAssignService(ApplicationDbContext context)
        {
            _context = context;
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


        public async Task<IEnumerable<ModuleAccessPage>> GetModuleAccessPages()
        {
            return await _context.moduleAccessPages.Include(x => x.eRPModule).ToListAsync();
        }

        public async Task<IEnumerable<ModuleAccessPageViewModel>> GetModuleAccessPagesactive(string UserTypeId)
        {
            List<Module> eRPModules = _context.modules.ToList();
            List<ModuleAccessPage> lstModuleAccess = _context.moduleAccessPages.Where(x => x.applicationRoleId == UserTypeId).ToList();
            List<ModuleAccessPageViewModel> data = new List<ModuleAccessPageViewModel>();
            foreach (Module d in eRPModules)
            {
                var Mdata = lstModuleAccess.Where(x => x.eRPModuleId == d.Id).ToList();
                int active = 0;
                if (Mdata.Count() > 0)
                {
                    active = 1;
                }
                data.Add(new ModuleAccessPageViewModel
                {
                    eRPModuleId = d.Id,
                    eRPModuleName = d.moduleName,
                    active = active

                });
            }
            return data;
        }

        public async Task<IEnumerable<Module>> GetERPModules()
        {
            return await _context.modules.ToListAsync();
        }
        public async Task<IEnumerable<Module>> GetERPModulesForTeam()
        {
            return await _context.modules.Where(x => x.isTeam == "Yes").AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid)
        {
            _context.moduleAccessPages.RemoveRange(_context.moduleAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
