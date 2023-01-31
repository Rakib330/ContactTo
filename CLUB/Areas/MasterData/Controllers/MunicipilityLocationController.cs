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
    public class MunicipilityLocationController : Controller
    {
        private readonly LangGenerate<MunicipalityInfoLn> _lang;
        // GET: MunicipilityLocation
        private readonly IReligionMunicipalityService religionMunicipalityService;

        public MunicipilityLocationController(IHostingEnvironment hostingEnvironment, IReligionMunicipalityService religionMunicipalityService)
        {
            _lang = new LangGenerate<MunicipalityInfoLn>(hostingEnvironment.ContentRootPath);
            this.religionMunicipalityService = religionMunicipalityService;
        }

        // GET: MunicipilityLocation
        public async Task<IActionResult> Index()
        {
            MunicipilityLocationViewModel model = new MunicipilityLocationViewModel
            {
                fLang = _lang.PerseLang("MasterData/MunicipilityEN.json", "MasterData/MunicipilityBN.json", Request.Cookies["lang"]),
                municipilityLocations = await religionMunicipalityService.GetMunicipilityLocation()
            };
            return View(model);
        }

        // POST: MunicipilityLocation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MunicipilityLocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/MunicipilityEN.json", "MasterData/MunicipilityBN.json", Request.Cookies["lang"]);
                model.municipilityLocations = await religionMunicipalityService.GetMunicipilityLocation();
                return View(model);
            }

            MunicipilityLocation data = new MunicipilityLocation
            {
                Id = model.municipalityId,
                locationName = model.locationName,
                locationNameBn = model.locationNameBn,
                shortName = model.shortName
            };

            await religionMunicipalityService.SaveMunicipilityLocation(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await religionMunicipalityService.DeleteMunicipilityLocationById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}