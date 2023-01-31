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
    public class ActivityStatusController : Controller
    {
        private readonly LangGenerate<ActivityStatusLn> _lang;
        private readonly IStatusService statusService;

        public ActivityStatusController(IHostingEnvironment hostingEnvironment, IStatusService statusService)
        {
            _lang = new LangGenerate<ActivityStatusLn>(hostingEnvironment.ContentRootPath);
            this.statusService = statusService;
        }
         
        // GET: ActivityStatus
        public async Task<IActionResult> Index()
        {
            ActivityStatusViewModel model = new ActivityStatusViewModel
            {
                fLang = _lang.PerseLang("MasterData/ActivityStatusEN.json", "MasterData/ActivityStatusBN.json", Request.Cookies["lang"]),
                activityStatus = await statusService.GetActivityStatus()
            };
            return View(model);
        }

        // POST: ActivityStatus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ActivityStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ActivityStatusEN.json", "MasterData/ActivityStatusBN.json", Request.Cookies["lang"]);
                model.activityStatus = await statusService.GetActivityStatus();
                return View(model);
            }

            ActivityStatus data = new ActivityStatus
            {
                Id = Int32.Parse(model.activityId),
                statusName = model.statusName,
                statusNameBn = model.statusNameBn,
                shortName = model.shortName
            };

            await statusService.SaveActivityStatus(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await statusService.DeleteActivityStatusById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}