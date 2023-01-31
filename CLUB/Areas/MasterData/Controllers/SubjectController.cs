using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class SubjectController : Controller
    {
        private readonly LangGenerate<SubjectInfoLn> _lang;
        private readonly ISubjectService subjectService;

        public SubjectController(IHostingEnvironment hostingEnvironment, ISubjectService subjectService)
        {
            _lang = new LangGenerate<SubjectInfoLn>(hostingEnvironment.ContentRootPath);
            this.subjectService = subjectService;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            SubjectViewModel model = new SubjectViewModel
            {
                fLang = _lang.PerseLang("MasterData/SubjectEN.json", "MasterData/SubjectBN.json", Request.Cookies["lang"]),
                subjects = await subjectService.GetAllSubject()
            };

            return View(model);
        }


        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SubjectViewModel model)
        {
            int validation = await subjectService.GetSubjectCheck(model.subjectId, model.subjectName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/SubjectEN.json", "MasterData/SubjectBN.json", Request.Cookies["lang"]);
                model.subjects = await subjectService.GetAllSubject();
                ModelState.AddModelError(string.Empty, "Subject Name '"+model.subjectName+"' already exists.");
                return View(model);
            }

            Subject data = new Subject
            {
                Id = model.subjectId,
                subjectName = model.subjectName,
                subjectNameBn = model.subjectNameBn,
                subjectShortName = model.subjectShortName
            };

            await subjectService.SaveSubject(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await subjectService.DeleteSubjectById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/getAllSubject")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            return Json(await subjectService.GetAllSubject());
        }
        #endregion
    }
}