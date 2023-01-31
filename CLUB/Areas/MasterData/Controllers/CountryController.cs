using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class CountryController : Controller
    {
        private readonly LangGenerate<CountryLn> _lang;
        private readonly IAddressService addressService;
        
        public CountryController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<CountryLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;            
        }
        // GET: Country
        public async Task<IActionResult> Index()
        {
            CountryViewModel model = new CountryViewModel
            {
                fLang = _lang.PerseLang("MasterData/CountryEN.json", "MasterData/CountryBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] CountryViewModel model)
        {
            int validation = await addressService.GetContryCheck(Int32.Parse(model.countryId), model.countryName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/CountryEN.json", "MasterData/CountryBN.json", Request.Cookies["lang"]);
                model.countries = await addressService.GetAllContry();
                ModelState.AddModelError(string.Empty, "Country name '" + model.countryName +"' already exists.");
                return View(model);
            }

            Country country = new Country
            {
                Id = Int32.Parse(model.countryId),
                countryCode = model.countryCode,
                shortName = model.shortName,
                countryName = model.countryName,
                countryNameBn = model.countryNameBn
            };

            await addressService.SaveCountry(country);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await addressService.DeleteContryById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }            
        }


        #region API Section
        [Route("global/api/countries")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            return Json(await addressService.GetAllContry());
        }
        #endregion
    }
}