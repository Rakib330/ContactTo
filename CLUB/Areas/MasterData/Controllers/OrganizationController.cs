using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class OrganizationController : Controller
    {
        private readonly LangGenerate<OrganizationLn> _lang;
        private readonly IOrganizationService organizationService;

        public OrganizationController(IHostingEnvironment hostingEnvironment, IOrganizationService organizationService)
        {
            _lang = new LangGenerate<OrganizationLn>(hostingEnvironment.ContentRootPath);
            this.organizationService = organizationService;
        }
        // GET: Organization
        public async Task<IActionResult> Index()
        {
            OrganizationViewModel model = new OrganizationViewModel
            {
                fLang = _lang.PerseLang("MasterData/OrganizationEN.json", "MasterData/OrganizationBN.json", Request.Cookies["lang"]),
                organizations = await organizationService.GetAllOrganization()
            };
            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OrganizationViewModel model)
        {
            int validation = await organizationService.GetOrganizationCheck(model.organizationId, model.organizationName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/OrganizationEN.json", "MasterData/OrganizationBN.json", Request.Cookies["lang"]);
                model.organizations = await organizationService.GetAllOrganization();
                ModelState.AddModelError(string.Empty, "Organization '"+model.organizationName+"' already exists.");
                return View(model);
            }

            Organization data = new Organization
            {
                Id = model.organizationId,
                organizationType = model.organizationType,
                organizationName = model.organizationName,
                organizationNameBn = model.organizationNameBn
            };

            await organizationService.SaveOrganization(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await organizationService.DeleteOrganizationById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/organizations")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Organizations()
        {
            return Json(await organizationService.GetAllOrganization());
        }
        #endregion
    }
}