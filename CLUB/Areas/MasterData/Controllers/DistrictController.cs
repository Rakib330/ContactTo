﻿using System;
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
    public class DistrictController : Controller
    {
        private readonly LangGenerate<DistrictLn> _lang;
        private readonly IAddressService addressService;

        public DistrictController(IHostingEnvironment hostingEnvironment, IAddressService addressService)
        {
            _lang = new LangGenerate<DistrictLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
        }
        // GET: District
        public async Task<IActionResult> Index()
        {
            DistrictViewModel model = new DistrictViewModel
            {
                fLang = _lang.PerseLang("MasterData/DistrictEN.json", "MasterData/DistrictBN.json", Request.Cookies["lang"]),
                districts =await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: District/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] DistrictViewModel model)
        {
            int validation = await addressService.GetDistrictCheck(model.districtId, model.districtName, model.divisionId);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/DistrictEN.json", "MasterData/DistrictBN.json", Request.Cookies["lang"]);
                model.districts = await addressService.GetAllDistrict();
                model.divisions = await addressService.GetAllDivision();
                model.countries = await addressService.GetAllContry();
                ModelState.AddModelError(string.Empty, "District Name '"+ model.districtName+ "' already exists.");
                return View(model);
            }

            District district = new District
            {
                Id = model.districtId,
                districtCode = model.districtCode,
                districtName = model.districtName,
                districtNameBn = model.districtNameBn,
                shortName = model.shortName,
                divisionId = model.divisionId
            };

            await addressService.SaveDistrict(district);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await addressService.DeleteDistrictById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/districts/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Districts(int Id)
        {
            return Json(await addressService.GetDistrictsByDivisonId(Id));
        }
        #endregion
    }
}