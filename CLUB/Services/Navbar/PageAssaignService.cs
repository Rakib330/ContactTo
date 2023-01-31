
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Navbar.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CLUB.Services.Navbar
{
    public class PageAssaignService: IPageAssaign
    {
        private readonly ApplicationDbContext _context;

        public PageAssaignService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeid)
        {
            _context.userAccessPages.RemoveRange(_context.userAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
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
    }
}
