using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.MasterData.Models;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class EventCategoryController : Controller
    {
        private readonly IEventCategoryService eventCategoryService;

        public EventCategoryController(IEventCategoryService eventCategoryService)
        {
            this.eventCategoryService = eventCategoryService;
        }

        // GET: EventCategory
        public async Task<IActionResult> Index()
        {
            EventCategoryViewModel model = new EventCategoryViewModel
            {
                eventCategories = await eventCategoryService.GetAllEventCategory(),
            };
            return View(model);
        }

        // POST: EventCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EventCategoryViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.eventCategories = await eventCategoryService.GetAllEventCategory();
                return View(model);
            }

            EventCategory data = new EventCategory
            {
                Id = model.categoryId,
                name = model.name,
                remarks = model.remarks
            };

            await eventCategoryService.SaveEventCategory(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await eventCategoryService.DeleteEventCategoryById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}