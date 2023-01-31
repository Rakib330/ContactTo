
using CLUB.Data.Entity.Navbar;
using CLUB.Models.Settings;
using CLUB.Services.Navbar.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club.Areas.Settings.Controllers
{
    [Authorize]
    [Area("Settings")]
    public class NavbarController : Controller
    {
        private readonly INavbar navbarService;
        private readonly IModuleAssaign moduleAssaign;
        public NavbarController(INavbar navbarService, IModuleAssaign moduleAssaign)
        {
            this.navbarService = navbarService;
            this.moduleAssaign = moduleAssaign;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Module()
        {
            var model = new ModuleViewModel
            {
                eRPModules = await navbarService.GetModules()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Module(ModuleViewModel model)
        {
            var data = new Module
            {
                Id = model.id,
                moduleName = model.nameEnglish,
                moduleNameBN = model.nameBangla,
                shortOrder = model.sortOrder
            };
            await navbarService.SaveModule(data);
            return RedirectToAction("Module");
        }

        public async Task<IActionResult> Create()
        {
            var model = new NavbarViewModel
            {
                ERPModules = await moduleAssaign.GetERPModules(),
                navbarsbyparent = await navbarService.GetNavbarItemByParent(),
                navbars = await navbarService.GetNavbarItem(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] NavbarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ERPModules = await moduleAssaign.GetERPModules();
                model.navbarsbyparent = await navbarService.GetNavbarItemByParent();
                model.navbars = await navbarService.GetNavbarItem();
                return View(model);
            }
            int? parentId = model.parentID;
            if (model.isParent == 2)
            {
                parentId = model.bandID;
            }

            Navbars data = new Navbars
            {
                Id = model.Id ?? 0,
                nameOption = model.nameOption,
                nameOptionBangla = model.nameOptionBangla,
                moduleId = model.moduleId,
                area = model.area,
                controller = model.controller,
                action = model.action,
                imageClass = model.imageClass,
                activeLi = model.activeLi,
                status = model.status,
                isParent = model.isParent,
                parentID = (int)parentId,
                displayOrder = model.displayOrder
            };

            await navbarService.SaveNavbarItem(data);

            return RedirectToAction(nameof(Create));
        }
        #region API Section
        [Route("global/api/getNavbarItemByParentByModule/{moduleId}/{isParent}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarItemByParentByModule(int moduleId, int isParent)
        {
            return Json(await navbarService.GetNavbarItemByParentByModule(moduleId, isParent));
        }
        [Route("global/api/GetNavbarItemByParentIdByModule/{moduleId}/{isParent}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarItemByParentIdByModule(int moduleId, int isParent)
        {
            return Json(await navbarService.GetNavbarItemByParentIdByModule(moduleId, isParent));
        }

        [Route("global/api/GetNavbarParentIdById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarParentIdById(int id)
        {
            return Json(await navbarService.GetNavbarParentIdById(id));
        }
        #endregion
    }
}
