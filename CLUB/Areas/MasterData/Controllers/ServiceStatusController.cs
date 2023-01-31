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
    public class ServiceStatusController : Controller
    {
        private readonly LangGenerate<ServiceStatusLn> _lang;
        // GET: ServiceStatus
        private readonly IStatusService statusService;

        public ServiceStatusController(IHostingEnvironment hostingEnvironment, IStatusService statusService)
        {
            _lang = new LangGenerate<ServiceStatusLn>(hostingEnvironment.ContentRootPath);
            this.statusService = statusService;
        }

        // GET: ServiceStatus
        public async Task<IActionResult> Index()
        {
            ServiceStatusViewModel model = new ServiceStatusViewModel
            {
                fLang = _lang.PerseLang("MasterData/ServiceStatusEN.json", "MasterData/ServiceStatusBN.json", Request.Cookies["lang"]),
                serviceStatus = await statusService.GetServiceStatus()
            };
            return View(model);
        }

        // POST: ServiceStatus/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ServiceStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/ServiceStatusEN.json", "MasterData/ServiceStatusBN.json", Request.Cookies["lang"]);
                model.serviceStatus = await statusService.GetServiceStatus();
                return View(model);
            }

            ServiceStatus data = new ServiceStatus
            {
                Id = model.serviceStatusId,
                statusName = model.statusName,
                statusNameBn = model.statusNameBn,
                statusShortName = model.statusShortName
            };

            await statusService.SaveServiceStatus(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await statusService.DeleteServiceStatusById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}