using Club.Services.MasterData.Interfaces;
using CLUB.Areas.Auth.Models;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Navbar;
using CLUB.Models.Auth;
using CLUB.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Club.Areas.Auth.Controllers
{
    [Authorize]
    [Area("Auth")]
    public class UserRoleAssignController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserInfos userInfoes;
        private readonly IERPModuleService eRPModuleService;
        private ApplicationDbContext _db;
        private readonly IPageAssignService pageAssignService;


        public UserRoleAssignController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, IUserInfos userInfoes, IPageAssignService pageAssignService, IERPModuleService eRPModuleService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.userInfoes = userInfoes;
            this.eRPModuleService = eRPModuleService;
            this.pageAssignService = pageAssignService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel model = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(model);
            }
            return View();
        }

		public async Task<IActionResult> AccessPage()
		{
			var model = new ApplicationRoleViewModel
			{
				modules = await pageAssignService.GetAllModulesByRoleId("1892e8fe-06ad-4e68-a46c-c409cd4bb4cd"),
				navbars = await pageAssignService.GetAllNavbars("1892e8fe-06ad-4e68-a46c-c409cd4bb4cd")
            };
			return View(model);
		}

        public async Task<IActionResult> GetNavbarsByRoleId(string roleId)
        {
            var model = new ApplicationRoleViewModel
            {
                modules = await pageAssignService.GetAllModulesByRoleId(roleId),
                navbars = await pageAssignService.GetAllNavbars(roleId),
                accessedNavbars = await pageAssignService.GetNavbarsByUserRoleId(roleId)
            };
            return Json(model);
        }

		public async Task<IActionResult> AssaignRoleToUser()
        {
            string userName = User.Identity.Name;
            var roles = await _roleManager.Roles.Where(x => x.Name != "SuperAdmin").ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(rolesModel);
            }

            ApplicationRoleViewModel model = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole,
            };
            return View(model);
        }
        public async Task<IActionResult> GetAllRoles()
        {
            string userName = User.Identity.Name;
            var roles = await _roleManager.Roles.Where(x => x.Name != "OPUSAdmin").ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(rolesModel);
            }

            ApplicationRoleViewModel model = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole,
            };
            return Json(model.roleViewModels);
        }

        public async Task<IActionResult> SavePageAccess(ApplicationRoleViewModel model)
        {
            await pageAssignService.DeleteUserAccesspageByUserTypeId(model.aspnetroleId);

            foreach (var item in model.navbarId)
            {
                var access = new UserAccessPage
                {
                    Id = 0,
                    navbarId = item,
                    applicationRoleId = model.aspnetroleId
                };
                await pageAssignService.SaveUserAccessPage(access);
            }


            return Json("success");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssaignRoleToUser([FromForm] ApplicationRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //return Json(model);
            var user = await _userManager.FindByNameAsync(model.userName);
            await userInfoes.DeleteUserRoleListByUserId(user.Id);
            if (model.roleIdList.Count() > 0)
            {
                for (int i = 0; i < model.roleIdList.Count(); i++)
                {
                    await _userManager.AddToRoleAsync(user, model.roleIdList[i]);
                }
            }

            return Json("created");
        }

        public IActionResult AdminSettings() {

            return View();
        }

        //public async Task<IActionResult> ClearData()
        //{
        //    await userInfoes.ClearAllData();
        //    return RedirectToAction("AdminSettings");
        //}


        #region API Section
        [Route("global/api/getUserInfoList")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            return Json(await userInfoes.GetUserInfo());
        }

        [Route("global/api/getUserInfoList/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserRoleInfoByUserId(string Id)
        {
            return Json(await userInfoes.GetRoleListByUserId(Id));
        }

        [Route("global/api/GetUserRoleByModule/{moduleId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserRoleByModule(int moduleId)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel rolesModel = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(rolesModel);
            }
            return Json(await userInfoes.GetUserInfo());
        }

        #endregion 
        [Route("global/api/getAllUserAccessPages/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllUserAccessPages(string Id)
        {
            return Json(await _db.userAccessPages.Where(x => x.applicationRoleId == Id).Include(x => x.navbar).Include(x => x.applicationRole).ToListAsync());
        }

        public async Task<JsonResult> GetUserMenuAccess(string userTypeId)
        {
            try
            {
                var data = await pageAssignService.GetUserMenuAccessByUserType(userTypeId);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<IActionResult> GetEmployeeInfoList()
        //{
        //    var employee = await userInfoes.GetAllUserInfos();
        //    return Json(employee.ToList());
        //}
    }

}
