using CLUB.Areas.Auth.Models;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Models.Auth;
using CLUB.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth
{
    public class LoggedinUser : ILoggedinUser
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public LoggedinUser(UserManager<ApplicationUser> userManage, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManage = userManage;
            _roleManager = roleManager;
            _context = context;
        }

        public int UserAuthId(string id)
        {
            return _context.employeeInfos.Where(x => x.ApplicationUserId == id).Select(x => x.Id).FirstOrDefault();
        }

        public string UserAuthUrl(int id)
        {
            var result= _context.photographs.Where(x => x.employeeId == id).Select(x => x.url).FirstOrDefault(); 
            return result;
        }

        public async Task<int> UserEmpId(string name)
        {
            ApplicationUser data = await _userManage.FindByNameAsync(name);
            return _context.employeeInfos.Where(x => x.ApplicationUserId == data.Id).Select(x => x.Id).FirstOrDefault();
        }

        public async Task<string> usersOrganization(string name)
        {
            ApplicationUser data =  await _userManage.FindByNameAsync(name);
            return data.org;
        }

        public async Task<List<string>> UsersRoles(string name)
        {
            return (List<string>) await _userManage.GetRolesAsync( await _userManage.FindByNameAsync(name));  
        }



        public async Task<IEnumerable<GetAllUserViewModel>> GetAllUserInfos()
        {
            var result = await _context.getAllUserViewModels.FromSql($"sp_GetAllUserInfos").ToListAsync();
            return result;
        }
        public async Task<string> GetRoleNatureByUserId(string userName)
        {
            var data = await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where u.UserName == userName
                              select r.Name).FirstOrDefaultAsync();

            return Convert.ToString(data);
        }
        public async Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosForProxyUser(string userName, string rolename)
        {
            try
            {
                var data = await _context.getAllUserViewNewModels.FromSql($"sp_GetAllUserInfosForProxyUser {userName},{rolename}").ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
