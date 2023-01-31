using CLUB.Areas.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Role
        public ActionResult Index()
        {
            _roleManager.Roles.Select(x => x.Name).ToList();

            RoleViewModel model = new RoleViewModel
            {
                roles = _roleManager.Roles.Select(x => x.Name).ToList()
            };

            return View(model);
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoleViewModel model)
        {
            

            if (!ModelState.IsValid)
            {
                model.roles = _roleManager.Roles.Select(x => x.Name).ToList();
                return View(model);
            }

            if (!await _roleManager.RoleExistsAsync(model.name))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = model.name });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}