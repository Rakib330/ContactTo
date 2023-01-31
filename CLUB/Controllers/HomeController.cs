using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CLUB.Models;
using Microsoft.AspNetCore.Authorization;
using CLUB.Helpers;
using CLUB.Models.Lang;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using CLUB.Data.Entity;
using CLUB.Services.Auth.Interfaces;
using CLUB.Services.Channel.Interfaces;
using Microsoft.Extensions.Configuration;
using DinkToPdf.Contracts;
using System.IO;
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using CLUB.Areas.Auth.Models;
using CLUB.Services.Employee.Interfaces;

namespace CLUB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly LangGenerate<HomeLn> _lang;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILoggedinUser loggedinUser;
        private readonly ISMSMailService iSMSMailService;
        private readonly IConfiguration _configuration;
        private readonly IPersonalInfoService personalInfoService;
        private readonly MyPDF myPDF;
        private readonly string rootPath;
        private ApplicationDbContext _db;
        private readonly IPageAssignService pageAssignService;
        private readonly INavbarService navbarService;
        public HomeController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, INavbarService navbarService, ApplicationDbContext db, IPageAssignService pageAssignService,
            IConfiguration _configuration,
            IConverter converter, ISMSMailService iSMSMailService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManage, ILoggedinUser loggedinUser)
        {
            _lang = new LangGenerate<HomeLn>(hostingEnvironment.ContentRootPath);
            _roleManager = roleManager;
            _userManage = userManage;
            _db = db;
            this.loggedinUser = loggedinUser;
            this.iSMSMailService = iSMSMailService;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            this._configuration = _configuration;
            rootPath = hostingEnvironment.ContentRootPath;
            this.pageAssignService = pageAssignService;
            this.navbarService = navbarService;
            this.personalInfoService = personalInfoService;
        }
        public async Task<IActionResult> Index(string Message = null)
        {
            string userName = HttpContext.User.Identity.Name;
            if (userName == "" || userName == null)
            {
                return RedirectToAction("RegisterClubnew", "Home");
            }
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);

            HomeViewModel model = new HomeViewModel
            {
                fLang = _lang.PerseLang("Home/HomeEN.json", "Home/HomeBN.json", Request.Cookies["lang"]),
                roles = (List<string>)await _userManage.GetRolesAsync(user),
                empOrganization = user.org,
                //id = loggedinUser.UserAuthId(user.Id),
                id = await personalInfoService.GetEmployeeIDByAuthIDint(user.UserName),
                Message = Message
            };

            if (user.UserName == "0006" || User.Identity.Name == "opus") /*user.UserName == "0012" || user.UserName == "0005" ||*/
            {
                return View(model);
            }
            else
            //if (agmInfo.isVerifed.GetValueOrDefault(0) == 3)
            //{
            //    return RedirectToAction("PaymentSlip", "Home", new { id = agmInfo.Id });
            //}
            {
                //return RedirectToAction("RegisterClubnew", "Home", new { id = agmInfo.Id });
                return RedirectToAction(nameof(MemberDashboard));
            }
            // return Json(model);
            //return View(model);

        }

        public async Task<IActionResult> MemberDashboard(string Message = null)
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);

            HomeViewModel model = new HomeViewModel
            {
                fLang = _lang.PerseLang("Home/HomeEN.json", "Home/HomeBN.json", Request.Cookies["lang"]),
                roles = (List<string>)await _userManage.GetRolesAsync(user),
                empOrganization = user.org,
                id = await personalInfoService.GetEmployeeIDByAuthIDint(user.UserName),
                Message = Message
            };
            return View(model);

        }



        public async Task<IActionResult> Navigation()
        {
            string userName = HttpContext.User.Identity.Name;

            NavbarViewModel model = new NavbarViewModel
            {
                navbars = await navbarService.GetNavigationMenu(userName),

            };
            ViewBag.UserTypeID = 1;

            return PartialView("_NavMenu", model);
        }

        public async Task<IActionResult> AssignPage()
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            List<int?> lstmodule = _db.userAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

            List<int> lstparentId = _db.navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();
            List<int> lstparentIdF = _db.navbars.Where(x => lstparentId.Contains(x.Id)).Select(x => x.parentID).ToList();

            var navdata = await pageAssignService.GetNavbars(userName);
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "1892e8fe-06ad-4e68-a46c-c409cd4bb4cd").ToList();
            if (adminrole.Count() == 0)
            {
                navdata = navdata.Where(x => lstmodule.Contains(x.Id) || lstparentId.Contains(x.Id) || lstparentIdF.Contains(x.Id));
            }
            List<int?> modid = navdata.Select(x => x.moduleId).ToList();
            var modules = await pageAssignService.GetERPModules();
            if (adminrole.Count() == 0)
            {
                modules = modules.Where(x => modid.Contains(x.Id));
            }
            NavbarViewModel model = new NavbarViewModel
            {
                navbars = navdata,//await pageAssignService.GetNavbars(userName),
                ERPModules = modules,//await pageAssignService.GetERPModules(),
                //userRoles = await _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync()
            };

            ViewBag.UserTypeID = 1;

            return PartialView("_Navbar", model);
        }


        public async Task<IActionResult> GridMenuPage(int moduleId, int perentId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            List<int?> lstmodule = _db.userAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

            List<int> lstparentId = _db.navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();

            List<Navbars> lstMenu = _db.navbars.Where(x => x.moduleId == moduleId && x.parentID == perentId && x.status == true).OrderBy(x => x.displayOrder).ToList();
            List<Navbars> lstChieldMenu = new List<Navbars>();

            var navdata = await pageAssignService.GetNavbars(userName);
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "E2F9E156-B4B3-4049-9815-6E6C99B95AEA").ToList();
            if (adminrole.Count() == 0)
            {
                lstChieldMenu = navdata.Where(x => lstmodule.Contains(x.Id)).ToList();
                lstMenu = lstMenu.Where(x => lstparentId.Contains(x.Id)).ToList();
            }
            else
            {
                lstChieldMenu = navdata.ToList();
            }
            List<int?> modid = navdata.Select(x => x.moduleId).ToList();
            var modules = await pageAssignService.GetERPModules();
            if (adminrole.Count() == 0)
            {
                modules = modules.Where(x => modid.Contains(x.Id));
            }

            var model = new NavbarViewModel
            {
                navbars = lstChieldMenu,
                navbarsbyparent = lstMenu,
                ERPModules = modules
            };
            return View(model);
        }


        public async Task<IActionResult> TestCard()
        {
            HomeViewModel model = new HomeViewModel
            {

            };
            // return Json(model);
            return View(model);
        }

        public async Task<IActionResult> BallonCard()
        {
            HomeViewModel model = new HomeViewModel
            {

            };
            // return Json(model);
            return View(model);
        }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz0123456789@#$%/!";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}