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
    public class PageAssignService : IPageAssignService
    {
        private readonly ApplicationDbContext _context;

        public PageAssignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveUserAccessPage(UserAccessPage userAccessPage)
        {
            try
            {
                if (userAccessPage.Id != 0)
                {
                    userAccessPage.updatedBy = userAccessPage.createdBy;
                    userAccessPage.updatedAt = DateTime.Now;
                    _context.userAccessPages.Update(userAccessPage);
                }
                else
                {
                    userAccessPage.createdAt = DateTime.Now;
                    _context.userAccessPages.Add(userAccessPage);
                }

                await _context.SaveChangesAsync();
                return userAccessPage.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<NavbarViewModel>> GetNavbarDataByUser(string userName, string lang)
        {
            return await _context.navbarViewModels.FromSql($"SP_GetuserMenu {userName},{lang}").ToListAsync();
        }

        public async Task<IEnumerable<Navbars>> GetNavbars(string userName)
        {
            return await _context.navbars.Include(x => x.module).Where(x => x.status == true).OrderBy(x => x.displayOrder).ToListAsync();
        }

        public async Task<IEnumerable<Module>> GetERPModules()
        {
            return await _context.modules.OrderBy(x => x.shortOrder).ToListAsync();
        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserType(string userTypeId)
        {
            try
            {
                var result = await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccess {userTypeId}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<NavbarWithRoleVm>> GetAllNavbars(string roleId)
        {
            var data = await _context.navbarWithRoleVms.FromSql($"sp_GetNavbarsWithAccess {roleId}").ToListAsync();

            return data;
        }
        public async Task<IEnumerable<Navbars>> GetNavbarsByUserRoleId(string roleId)
        {
            var data = new List<Navbars>();
            if (roleId == "1892e8fe-06ad-4e68-a46c-c409cd4bb4cd")
            {
                data = await _context.userAccessPages.Where(x => x.navbar.status == true).OrderBy(x => x.navbar.displayOrder).Select(x => x.navbar).Distinct().AsNoTracking().ToListAsync();
            }
            else
            {
                data = await _context.userAccessPages.Where(x => x.navbar.status == true && x.applicationRoleId == roleId).OrderBy(x => x.navbar.displayOrder).Select(x => x.navbar).Distinct().AsNoTracking().ToListAsync();
            }
            return data;
        }

        public async Task<IEnumerable<Module>> GetAllModulesByRoleId(string roleId)
        {
            var data = new List<Module>();

            if (roleId == "1892e8fe-06ad-4e68-a46c-c409cd4bb4cd")
            {
                data = await _context.moduleAccessPages.Select(x => x.eRPModule).Distinct().AsNoTracking().ToListAsync();
            }
            else
            {
                data = await _context.moduleAccessPages.Where(x => x.applicationRoleId == roleId).OrderBy(x => x.eRPModule.shortOrder).Select(x => x.eRPModule).Distinct().AsNoTracking().ToListAsync();
            }
            return data;
        }

        public async Task<IEnumerable<string>> GetUserMenuAccessByRoles(string[] roles)
        {
            try
            {
                var result = await (from r in _context.Roles
                                    join p in _context.userAccessPages
                                    on r.Id equals p.applicationRoleId
                                    join n in _context.navbars
                                    on p.navbarId equals n.Id
                                    where roles.Contains(r.Name)
                                    select n.nameOption).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByModule(string userTypeId, int ModuleId)
        {
            try
            {
                var result = await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessByModule {userTypeId},{ModuleId}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByMenu(string userTypeId, int ModuleId)
        {
            try
            {
                var result = await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessByMenu {userTypeId},{ModuleId}").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeModule(string userTypeId, string userTypeIdM)
        {
            return await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessModule {userTypeId},{userTypeIdM}").ToListAsync();
        }
      

        public async Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeid)
        {
            _context.userAccessPages.RemoveRange(_context.userAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
        }
        
    }
}
