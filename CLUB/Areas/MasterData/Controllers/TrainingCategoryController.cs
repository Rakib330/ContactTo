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
    public class TrainingCategoryController : Controller
    {
        private readonly LangGenerate<TrainingCategoryLn> _lang;
        private readonly ITrainingService trainingService;

        public TrainingCategoryController(IHostingEnvironment hostingEnvironment, ITrainingService trainingService)
        {
            _lang = new LangGenerate<TrainingCategoryLn>(hostingEnvironment.ContentRootPath);
            this.trainingService = trainingService;
        }
        // GET: TrainingCategory
        public async Task<IActionResult> Index()
        {
            TrainingCategoryViewModel model = new TrainingCategoryViewModel
            {
                fLang = _lang.PerseLang("MasterData/TrainingCategoryEN.json", "MasterData/TrainingCategoryBN.json", Request.Cookies["lang"]),
                trainingCategories = await trainingService.GetTrainingCategories()
            };
            return View(model);
        }

        // POST: TrainingCategory/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/TrainingCategoryEN.json", "MasterData/TrainingCategoryBN.json", Request.Cookies["lang"]);
                model.trainingCategories = await trainingService.GetTrainingCategories();
                return View(model);
            }

            TrainingCategory data = new TrainingCategory
            {
                Id = model.trnCategoryId,
                trainingCategoryName = model.trainingCategoryName,
                trainingCategoryNameBn = model.trainingCategoryNameBn,
                trainingCategoryShortName = model.trainingCategoryShortName
            };

            await trainingService.SaveTrainingCategory(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await trainingService.DeleteTrainingCategoryById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/getTrainingCategories")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTrainingCategories()
        {
            return Json(await trainingService.GetTrainingCategories());
        }
        #endregion
    }
}