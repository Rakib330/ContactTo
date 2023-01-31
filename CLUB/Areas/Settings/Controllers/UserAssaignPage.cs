
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Navbar;
using CLUB.Services.Auth.Interfaces;
using CLUB.Services.Navbar.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club.Areas.Settings.Controllers
{
    [Authorize]
    [Area("Settings")]
    public class UserAssaignPage : Controller
    {
        private readonly IPageAssaign pageAssaign;
        private readonly IModuleAssaign moduleAssaign;
        private readonly INavbar navbarService;
        private readonly IUserInfos userInfos;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAssaignPage(IPageAssaign pageAssaign, IModuleAssaign moduleAssaign, INavbar navbarService, IUserInfos userInfos, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.pageAssaign = pageAssaign;
            this.moduleAssaign = moduleAssaign;
            this.navbarService = navbarService;
            this.userInfos = userInfos;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveUserMenuAccess(string UserTypeIds, int[] AllMenuIds)
        {
            await pageAssaign.DeleteUserAccesspageByUserTypeId(UserTypeIds);

            foreach (var app in AllMenuIds)
            {
                UserAccessPage UAP = new UserAccessPage();
                UAP.Id = 0;
                UAP.applicationRoleId = UserTypeIds;
                UAP.isAccess = 1;
                UAP.navbarId = Convert.ToInt32(app);

                await pageAssaign.SaveUserAccessPage(UAP);
                UAP = new UserAccessPage();

            }


            return Json(new { result = "1" });
        }
        [HttpPost]
        public async Task<JsonResult> SaveUserModuleAccess(string UserTypeIds, int[] AllMenuIds)
        {
            await moduleAssaign.DeleteModuleAccesspageByUserTypeId(UserTypeIds);

            foreach (var app in AllMenuIds)
            {
                ModuleAccessPage UAP = new ModuleAccessPage();
                UAP.Id = 0;
                UAP.applicationRoleId = UserTypeIds;

                UAP.eRPModuleId = Convert.ToInt32(app);

                await moduleAssaign.SaveModuleAccessPage(UAP);
                UAP = new ModuleAccessPage();

            }


            return Json(new { result = "1" });
        }
        public async Task<IActionResult> GetUserModule()
        {
            var result = await navbarService.GetAllModules();
            return Json(result);
        }

        public async Task<IActionResult> GetUserMenus(int id)
        {
            var result = await navbarService.GetNavbarItemByModule(id);
            return Json(result);
        }
        public async Task<IActionResult> GetUserType()
        {
            string userName = HttpContext.User.Identity.Name;
            string[] adminRoles = { "0583d54e-74a8-46a3-b880-e13698723f69", "31a586e8-f6d8-498f-bc8c-42ed33c7182f" };
            var user = await userInfos.GetUserByUserName(userName);
            var userId = user.Id;
            List<string> roleids = await userInfos.GetRoleIdByUserId(userId);
            var roles = await _roleManager.Roles.Where(x => !adminRoles.Contains(x.Id)).ToListAsync();
            var result = roles;
            return Json(result);
        }
        [Route("global/api/getAllUserAccessPages/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllUserAccessPages(string Id)
        {
            var data = await navbarService.GetAllUserAccessPages(Id);
            return Json(data);
        }
    }
}
