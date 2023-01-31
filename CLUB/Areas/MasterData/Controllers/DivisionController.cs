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
    public class DivisionController : Controller
    {
        private readonly LangGenerate<DivisionLn> _lang;
        private readonly IAddressService addressService;

        public DivisionController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<DivisionLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: Division
        public async Task<IActionResult> Index()
        {
            DivisionViewModel model = new DivisionViewModel
            {
                fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]),
                divisions = await addressService.GetAllDivision(),
                countries=await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Division/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DivisionViewModel model)
        {
            int validation = await addressService.GetDivisionCheck(model.divisionId, model.divisionName, model.countryId);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/DivisionEN.json", "MasterData/DivisionBN.json", Request.Cookies["lang"]);
                model.divisions = await addressService.GetAllDivision();
                model.countries = await addressService.GetAllContry();
                ModelState.AddModelError(string.Empty, "Division Name'"+ model.divisionName + "' already exists.");
                return View(model);
            }


            Division division = new Division
            {
                Id = model.divisionId,
                divisionCode = model.divisionCode,
                divisionName = model.divisionName,
                divisionNameBn = model.divisionNameBn,
                shortName = model.shortName,
                countryId=model.countryId
            };

            await addressService.SaveDivision(division);

            return RedirectToAction(nameof(Index));

        }

        // GET: Division/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Division/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await addressService.DeleteDivisionById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/divisions/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Divisions(int Id)
        {
            return Json(await addressService.GetDivisionsByCountryId(Id));
        }
        #endregion

    }
}