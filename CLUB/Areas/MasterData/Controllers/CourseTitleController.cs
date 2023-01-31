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
    public class CourseTitleController : Controller
    {
        private readonly LangGenerate<CourseTitleLn> _lang;
        private readonly IYearCourseTitleService yearCourseTitleService;

        public CourseTitleController(IHostingEnvironment hostingEnvironment, IYearCourseTitleService yearCourseTitleService)
        {
            _lang = new LangGenerate<CourseTitleLn>(hostingEnvironment.ContentRootPath);
            this.yearCourseTitleService = yearCourseTitleService;
        }

        // GET: CourseTitle
        public async Task<IActionResult> Index()
        {
            CourseTitleViewModel model = new CourseTitleViewModel
            {
                fLang = _lang.PerseLang("MasterData/CourseTitleEN.json", "MasterData/CourseTitleBN.json", Request.Cookies["lang"]),
                courseTitles = await yearCourseTitleService.GetCourseTitle()
            };
            return View(model);
        }

        // POST: CourseTitle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] CourseTitleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/CourseTitleEN.json", "MasterData/CourseTitleBN.json", Request.Cookies["lang"]);
                model.courseTitles = await yearCourseTitleService.GetCourseTitle();
                return View(model);
            }

            CourseTitle data = new CourseTitle
            {
                Id = Int32.Parse(model.courseTitleId),
                nameEN = model.courseTitle
            };

            await yearCourseTitleService.SaveCourseTitle(data);

            return RedirectToAction(nameof(Index));

        }


        //xxxxxxxxxxxxxxxx
    }
}