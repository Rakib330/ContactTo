
using Club.Services.MasterData.Interfaces;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Data.Entity.Navbar;
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


namespace CLUB.Areas.Auth.Controllers
{
    [Authorize]
    [Area("Auth")]
    public class UserAssignPageController : Controller
    {
        private readonly IPageAssignService pageAssignService;
        private readonly IModuleAssignService moduleAssignService;
        private readonly IUserInfos userInfoes;
        private readonly IERPModuleService eRPModuleService;
        private readonly INavbarService navbarServices;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _db;

        public UserAssignPageController(IPageAssignService pageAssignService, INavbarService navbarServices, IERPModuleService eRPModuleService, ApplicationDbContext db, IModuleAssignService moduleAssignService
            , IUserInfos userInfoes,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.pageAssignService = pageAssignService;
            this.userInfoes = userInfoes;
            this.moduleAssignService = moduleAssignService;
            this.eRPModuleService = eRPModuleService;
            this.navbarServices = navbarServices;
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexModule()
        {
            return View();
        }
        public IActionResult ModuleAssign()
        {
            return View();
        }
        public IActionResult ModuleAssignERP()
        {
            return View();
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

        public async Task<JsonResult> GetUserMenuAccessByModule(string userTypeId, int moduleId)
        {
            try
            {
                var data = await pageAssignService.GetUserMenuAccessByUserTypeByModule(userTypeId, moduleId);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonResult> GetUserMenuAccessByUserTypeByMenu(string userTypeId, int moduleId)
        {
            try
            {
                var data = await pageAssignService.GetUserMenuAccessByUserTypeByMenu(userTypeId, moduleId);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonResult> GetUserModuleAccess(string userTypeId)
        {
            try
            {
                var data = await moduleAssignService.GetModuleAccessPagesactive(userTypeId);
                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonResult> GetUserModuleAccessERP(string userTypeId)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
                // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
                List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
                var datamodule = await moduleAssignService.GetModuleAccessPages();
                List<int?> datamoduleId = datamodule.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.eRPModuleId).ToList();
                var data = await moduleAssignService.GetModuleAccessPagesactive(userTypeId);
                return Json(data.Where(x => datamoduleId.Contains(x.eRPModuleId)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonResult> GetUserMenuAccessModule(string userTypeId)
        {
            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
                // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
                string roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefault();
                var data = await pageAssignService.GetUserMenuAccessByUserTypeModule(userTypeId, roleids);

                return Json(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveUserMenuAccess(string UserTypeIds, int[] AllMenuIds)
        {
            await pageAssignService.DeleteUserAccesspageByUserTypeId(UserTypeIds);

            foreach (var app in AllMenuIds)
            {
                UserAccessPage UAP = new UserAccessPage();
                UAP.Id = 0;
                UAP.applicationRoleId = UserTypeIds;
                UAP.isAccess = 1;
                UAP.navbarId = Convert.ToInt32(app);

                await pageAssignService.SaveUserAccessPage(UAP);
                UAP = new UserAccessPage();

            }


            return Json(new { result = "1" });
        }
        [HttpPost]
        public async Task<JsonResult> SaveUserModuleAccess(string UserTypeIds, int[] AllMenuIds)
        {
            await moduleAssignService.DeleteModuleAccesspageByUserTypeId(UserTypeIds);

            foreach (var app in AllMenuIds)
            {
                ModuleAccessPage UAP = new ModuleAccessPage();
                UAP.Id = 0;
                UAP.applicationRoleId = UserTypeIds;

                UAP.eRPModuleId = Convert.ToInt32(app);

                await moduleAssignService.SaveModuleAccessPage(UAP);
                UAP = new ModuleAccessPage();

            }


            return Json(new { result = "1" });
        }

        public async Task<IActionResult> GetUserType()
        {
            //string userName = HttpContext.User.Identity.Name;
            //string[] adminRoles = { "1CD67294-7CE4-4054-AB62-71DB12935F34", "E2F9E156-B4B3-4049-9815-6E6C99B95AEA" };
            //var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            //List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
             
            //var roles = await _roleManager.Roles.Where(x => !adminRoles.Contains(x.Id)).ToListAsync();
            var result = await  _roleManager.Roles.ToListAsync(); // roles; //await userInfoes.GetUserTypeList();
            return Json(result);
        }

        public async Task<IActionResult> GetUserModule()
        {
            var result = await eRPModuleService.GetAllERPModule();
            return Json(result);
        }

        public async Task<IActionResult> GetUserMenus(int id)
        {
            var result = await navbarServices.GetNavbarItemByModule(id);
            return Json(result);
        }

        public async Task<IActionResult> GetUserTypeERP()
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();

            var roles = await _roleManager.Roles.Where(x => !roleids.Contains(x.Id) && x.Name != "admin" && x.Name != "SuperAdmin").ToListAsync();
            var result = roles; //await userInfoes.GetUserTypeList();
            return Json(result);
        }

        [Route("global/api/getAllUserAccessPages/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getAllUserAccessPages(string Id)
        {
            return Json(await _db.userAccessPages.Where(x => x.applicationRoleId == Id).Include(x => x.navbar).Include(x => x.applicationRole).ToListAsync());
        }

    }
}
