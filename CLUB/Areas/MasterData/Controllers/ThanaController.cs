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
    public class ThanaController : Controller
    {
        private readonly LangGenerate<ThanaInfoLn> _lang;
        private readonly IAddressService addressService;

        public ThanaController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<ThanaInfoLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Thana
        public async Task<IActionResult> Index()
        {
            ThanaViewModel model = new ThanaViewModel
            {
                fLang = _lang.PerseLang("MasterData/ThanaEN.json", "MasterData/ThanaBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry(),
                thanas=await addressService.GetAllThana(),
            };
            return View(model);
        }

        // POST: Thana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ThanaViewModel model)
        {
            int validation =await addressService.GetThanaCheck(model.thanaId,model.thanaName, model.districtId);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/ThanaEN.json", "MasterData/ThanaBN.json", Request.Cookies["lang"]);
                model.countries = await addressService.GetAllContry();
                ModelState.AddModelError(string.Empty, "Thana Name '"+model.thanaName+ "' already exists.");
                model.thanas = await addressService.GetAllThana();
                return View(model);
            }

            Thana thana = new Thana
            {
                Id = model.thanaId,
                thanaCode = model.thanaCode,
                thanaName = model.thanaName,
                thanaNameBn = model.thanaNameBn,
                shortName = model.shortName,
                districtId = model.districtId
            };

            await addressService.SaveThana(thana);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await addressService.DeleteThanaById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/thanas/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Thanas(int Id)
        {
            return Json(await addressService.GetThanasByDistrictId(Id));
        }
        #endregion
    }
}