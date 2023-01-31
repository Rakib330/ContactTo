using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManage, RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManage;
            _roleManager = roleManager;
        }



        // GET: User
        public async Task<IActionResult> Index()
        {
            UserViewModel model = new UserViewModel
            {
                roles = _roleManager.Roles.Select(x=> x.Name).ToList(),
            };

            return View(model);
        }

        // POST: User/Inxed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserViewModel model)
        {
            //return Json(model);

            if (!ModelState.IsValid || model.UserId ==0)
            {
                model.roles = _roleManager.Roles.Select(x => x.Name).ToList();
                return View(model);
            }

            if (await _userManage.FindByNameAsync(model.Name) == null)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Name, org = model.organization };

                await _userManage.CreateAsync(user, model.Password);

                await _userManage.AddToRoleAsync(user, model.role);

                ApplicationUser applicationUser = await _userManage.FindByNameAsync(model.Name);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}