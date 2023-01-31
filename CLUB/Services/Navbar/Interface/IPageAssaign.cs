
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Navbar.Interface
{
    public interface IPageAssaign
    {
        Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeid);
        Task<int> SaveUserAccessPage(UserAccessPage userAccessPage);
    }
}
