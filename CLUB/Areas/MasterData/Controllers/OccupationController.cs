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
    public class OccupationController : Controller
    {
        private readonly LangGenerate<OccupationLn> _lang;
        private readonly IOccupationCadreService occupationCadreService;

        public OccupationController(IHostingEnvironment hostingEnvironment, IOccupationCadreService occupationCadreService)
        {
            _lang = new LangGenerate<OccupationLn>(hostingEnvironment.ContentRootPath);
            this.occupationCadreService = occupationCadreService;
        }
        // GET: Occupation
        public async Task<IActionResult> Index()
        {
            OccupationViewModel model = new OccupationViewModel
            {
                fLang = _lang.PerseLang("MasterData/OccupationEN.json", "MasterData/OccupationBN.json", Request.Cookies["lang"]),
                occupations = await occupationCadreService.GetOccupationInfo()
            };
            return View(model);
        }

        // POST: Occupation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OccupationViewModel model)
        {
            int validation = await occupationCadreService.GetOccupationCheck(model.occupationId, model.occupationName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/OccupationEN.json", "MasterData/OccupationBN.json", Request.Cookies["lang"]);
                model.occupations = await occupationCadreService.GetOccupationInfo();
                ModelState.AddModelError(string.Empty, "Occupation '"+model.occupationName+"' already exists.");
                return View(model);
            }

            Occupation data = new Occupation
            {
                Id = model.occupationId,
                occupationName = model.occupationName,
                occupationNameBn = model.occupationNameBn,
                occupationShortName = model.occupationShortName
            };

            await occupationCadreService.SaveOccupationInfo(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await occupationCadreService.DeleteOccupationInfoById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}