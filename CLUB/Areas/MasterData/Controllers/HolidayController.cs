using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.MasterData.Models;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class HolidayController : Controller
    {
        private readonly IHolidayService holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            this.holidayService = holidayService;
        }

        // GET: LevelofEducation
        public async Task<IActionResult> Index()
        {
            HolidayViewModel model = new HolidayViewModel
            {
                holidays = await holidayService.GetAllHoliday()
            };
            return View(model);
        }

        // POST: LevelofEducation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HolidayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.holidays = await holidayService.GetAllHoliday();
                return View(model);
            }

            Holiday data = new Holiday
            {
                holidayName = model.holidayName,
                holidayNameBn = model.holidayNameBn,
                weeklyHoliday = model.weeklyHoliday,
                year = Int32.Parse(model.year)
            };

            await holidayService.SaveHoliday(data);

            return RedirectToAction(nameof(Index));
        }
    }
}
