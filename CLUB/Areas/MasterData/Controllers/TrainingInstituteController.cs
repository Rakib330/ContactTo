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
    public class TrainingInstituteController : Controller
    {
        private readonly LangGenerate<TrainigInstituteLn> _lang;
        private readonly ITrainingService trainingService;

        public TrainingInstituteController(IHostingEnvironment hostingEnvironment, ITrainingService trainingService)
        {
            _lang = new LangGenerate<TrainigInstituteLn>(hostingEnvironment.ContentRootPath);
            this.trainingService = trainingService;
        }
        // GET: TrainingInstitute
        public async Task<IActionResult> Index()
        {
            TrainingInstituteViewModel model = new TrainingInstituteViewModel
            {
                fLang = _lang.PerseLang("MasterData/TrainingInstituteEN.json", "MasterData/TrainingInstituteBN.json", Request.Cookies["lang"]),
                trainingInstitutes = await trainingService.GetTrainingInstitute()
            };
            return View(model);
        }

        // POST: TrainingInstitute/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingInstituteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/TrainingInstituteEN.json", "MasterData/TrainingInstituteBN.json", Request.Cookies["lang"]);
                model.trainingInstitutes = await trainingService.GetTrainingInstitute();
                return View(model);
            }

            TrainingInstitute data = new TrainingInstitute
            {
                Id = model.trnInstituteId,
                trainingInstituteName = model.trainingInstituteName,
                trainingInstituteNameBn = model.trainingInstituteNameBn,
                trainingInstituteShortName = model.trainingInstituteShortName
            };

            await trainingService.SaveTrainingInstitute(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await trainingService.DeleteTrainingInstituteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/getTrainingInstitute")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTrainingInstitute()
        {
            return Json(await trainingService.GetTrainingInstitute());
        }
        #endregion
    }
}