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
    public class ReligionController : Controller
    {
        private readonly LangGenerate<ReligionLn> _lang;
        // GET: Religion
        private readonly IReligionMunicipalityService religionMunicipalityService;

        public ReligionController(IHostingEnvironment hostingEnvironment, IReligionMunicipalityService religionMunicipalityService)
        {
            _lang = new LangGenerate<ReligionLn>(hostingEnvironment.ContentRootPath);
            this.religionMunicipalityService = religionMunicipalityService;
        }

        // GET: Religion
        public async Task<IActionResult> Index()
        {
            ReligionViewModel model = new ReligionViewModel
            {
                fLang = _lang.PerseLang("MasterData/ReligionEN.json", "MasterData/ReligionBN.json", Request.Cookies["lang"]),
                religions = await religionMunicipalityService.GetReligions()
            };
            return View(model);
        }

        // POST: Religion/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ReligionViewModel model)
        {
            int validation = await religionMunicipalityService.GetReligionCheck(model.religionId, model.name);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/ReligionEN.json", "MasterData/ReligionBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                ModelState.AddModelError(string.Empty, "Religion name '" +model.name+ "' already exists.");
                return View(model);
            }

            Religion data = new Religion
            {
                Id = model.religionId,
                name = model.name,
                nameBn = model.nameBn,
                shortName = model.shortName
            };

            await religionMunicipalityService.SaveReligion(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await religionMunicipalityService.DeleteReligionById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/getReligions")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetReligions()
        {
            return Json(await religionMunicipalityService.GetReligions());
        }
        #endregion
    }
}