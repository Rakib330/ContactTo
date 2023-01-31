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
    public class YearController : Controller
    {
        // GET: Year
        private readonly LangGenerate<YearLn> _lang;
        private readonly IYearCourseTitleService yearCourseTitleService;

        public YearController(IHostingEnvironment hostingEnvironment, IYearCourseTitleService yearCourseTitleService)
        {
            _lang = new LangGenerate<YearLn>(hostingEnvironment.ContentRootPath);
            this.yearCourseTitleService = yearCourseTitleService;
        }

        // GET: CourseTitle
        public async Task<IActionResult> Index()
        {
            YearViewModel model = new YearViewModel
            {
                fLang = _lang.PerseLang("MasterData/YearEN.json", "MasterData/YearBN.json", Request.Cookies["lang"]),
                years = await yearCourseTitleService.GetYear()
            };
            return View(model);
        }

        // POST: CourseTitle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] YearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/YearEN.json", "MasterData/YearBN.json", Request.Cookies["lang"]);
                model.years = await yearCourseTitleService.GetYear();
                return View(model);
            }

            Year data = new Year
            {
                Id = Int32.Parse(model.yearId),
                year = model.yearTitle
            };

            await yearCourseTitleService.SaveYear(data);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await yearCourseTitleService.DeleteYearById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //xxxxxxxxxxxxxxxx
    }
}