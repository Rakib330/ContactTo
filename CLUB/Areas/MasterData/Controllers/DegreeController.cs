using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class DegreeController : Controller
    {
        private readonly LangGenerate<DegreeInfoLn> _lang;
        private readonly IDegreeService degreeService;

        private readonly ILevelofEducationService levelofEducationService;

        public DegreeController(IHostingEnvironment hostingEnvironment, IDegreeService degreeService, ILevelofEducationService levelofEducationService)
        {
            _lang = new LangGenerate<DegreeInfoLn>(hostingEnvironment.ContentRootPath);
            this.degreeService = degreeService;
            this.levelofEducationService = levelofEducationService;
        }

        // GET: Degree
        public async Task<IActionResult> Index()
        {
            DegreeViewModel model = new DegreeViewModel
            {
                fLang = _lang.PerseLang("MasterData/DegreeEN.json", "MasterData/DegreeBN.json", Request.Cookies["lang"]),
                degrees = await degreeService.GetAllDegree(),
                levelofeducationNamelist = await levelofEducationService.GetAllLevelofEducation()
            };


            return View(model);
        }

        // POST: Degree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DegreeViewModel model)
        {
            int validation = await degreeService.GetDegreeCheck(Int32.Parse(model.degreeId), model.degreeName,model.levelofeducationId);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/DegreeEN.json", "MasterData/DegreeBN.json", Request.Cookies["lang"]);
                model.degrees = await degreeService.GetAllDegree();
                model.levelofeducationNamelist = await levelofEducationService.GetAllLevelofEducation();
                ModelState.AddModelError(string.Empty, "Degree Name '"+ model.degreeName +"' already exists.");
                return View(model);
            }

            Degree data = new Degree
            {
                Id = Int32.Parse(model.degreeId),
                degreeName = model.degreeName,
                degreeNameBn = model.degreeNameBn,
                degreeShortName = model.degreeShortName,
                levelofeducationId=model.levelofeducationId
            };

            await degreeService.SaveDegree(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await degreeService.DeleteDegreeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/degrees/{id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Degrees(int Id)
        {
            return Json(await degreeService.GetDegreeByLOEId(Id));
        }

        [Route("global/api/getAllDegree")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDegree()
        {
            return Json(await degreeService.GetAllDegree());
        }
        #endregion
    }
}