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
    public class RelDegreeSubjectController : Controller
    {
        private readonly LangGenerate<DegSubRelationInfoLn> _lang;
        private readonly IRelDegreeSubjectService degreeSubjectService;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;

        public RelDegreeSubjectController(IHostingEnvironment hostingEnvironment, IRelDegreeSubjectService degreeSubjectService, IDegreeService degreeService, ISubjectService subjectService)
        {
            _lang = new LangGenerate<DegSubRelationInfoLn>(hostingEnvironment.ContentRootPath);
            this.degreeSubjectService = degreeSubjectService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
        }
        // GET: RelDegreeSubject
        public async Task<IActionResult> Index()
        {
            RelDegreeSubjectViewModel model = new RelDegreeSubjectViewModel
            {
                fLang = _lang.PerseLang("MasterData/DegreeSubjectEN.json", "MasterData/DegreeSubjectBN.json", Request.Cookies["lang"]),
                relDegreeSubjectslist = await degreeSubjectService.GetAllDegreeSubject(),
                degreeslist = await degreeService.GetAllDegree(),
                subjectslist = await subjectService.GetAllSubject()
            };
            return View(model);
        }

        // POST: RelDegreeSubject/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RelDegreeSubjectViewModel model)
        {
            int validation = await degreeSubjectService.GetdegreeSubjectCheck(model.subjectId, model.degreeId);
            //return Json(validation);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/DegreeSubjectEN.json", "MasterData/DegreeSubjectBN.json", Request.Cookies["lang"]);
                model.relDegreeSubjectslist = await degreeSubjectService.GetAllDegreeSubject();
                model.degreeslist = await degreeService.GetAllDegree();
                model.subjectslist = await subjectService.GetAllSubject();
                ModelState.AddModelError(string.Empty, "Relation degree subject already exists.");
                return View(model);
            }

            RelDegreeSubject data = new RelDegreeSubject
            {
                Id= Int32.Parse(model.relDegSubId),
                degreeId = model.degreeId,
                subjectId = model.subjectId
            };

            await degreeSubjectService.SaveDegreeSubject(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await degreeSubjectService.DeleteDegreeSubjectById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/relDegreeSubjects/{id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RelDegreeSubjects(int Id)
        {
            return Json(await degreeSubjectService.GetSubjectByDegreeId(Id));
        }
        #endregion
    }
}