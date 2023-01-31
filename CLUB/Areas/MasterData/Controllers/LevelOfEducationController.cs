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
    public class LevelOfEducationController : Controller
    {
        private readonly LangGenerate<LevelOfEducationLn> _lang;
        private readonly ILevelofEducationService levelofEducationService;

        public LevelOfEducationController(IHostingEnvironment hostingEnvironment, ILevelofEducationService levelofEducationService)
        {
            _lang = new LangGenerate<LevelOfEducationLn>(hostingEnvironment.ContentRootPath);
            this.levelofEducationService = levelofEducationService;
        }
        // GET: LevelofEducation
        public async Task<IActionResult> Index()
        {
            LevelofEducationViewModel model = new LevelofEducationViewModel
            {
                fLang = _lang.PerseLang("MasterData/LevelofEducationEN.json", "MasterData/LevelofEducationBN.json", Request.Cookies["lang"]),
                levelofEducations = await levelofEducationService.GetAllLevelofEducation()
            };
            return View(model);
        }

        // POST: LevelofEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LevelofEducationViewModel model)
        {
            int validation = await levelofEducationService.GetLevelofEducationCheck(model.loeId, model.levelofeducationName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/LevelofEducationEN.json", "MasterData/LevelofEducationBN.json", Request.Cookies["lang"]);
                model.levelofEducations = await levelofEducationService.GetAllLevelofEducation();
                ModelState.AddModelError(string.Empty, "Level of Education '"+ model.levelofeducationName+ "' already exists.");
                return View(model);
            }

            LevelofEducation data = new LevelofEducation
            {
                Id= model.loeId,
                levelofeducationName = model.levelofeducationName,
                levelofeducationNameBn = model.levelofeducationNameBn,
            };

            await levelofEducationService.SaveLevelofEducation(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await levelofEducationService.DeleteLevelofEducationById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/levelOfEducations")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LevelOfEducations()
        {
            return Json(await levelofEducationService.GetAllLevelofEducation());
        }
        #endregion
    }
}