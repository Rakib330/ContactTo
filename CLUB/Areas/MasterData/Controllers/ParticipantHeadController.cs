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
    public class ParticipantHeadController : Controller
    {
        private readonly IParticipantHeadService participantHeadService;

        public ParticipantHeadController(IParticipantHeadService participantHeadService)
        {
            this.participantHeadService = participantHeadService;
        }

        // GET: EventCategory
        public async Task<IActionResult> Index()
        {
            ParticipantHeadViewModel model = new ParticipantHeadViewModel
            {
                participantHeads = await participantHeadService.GetAllParticipantHead(),
            };
            return View(model);
        }

        // POST: EventCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ParticipantHeadViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.participantHeads = await participantHeadService.GetAllParticipantHead();
                return View(model);
            }

            ParticipantHead data = new ParticipantHead
            {
                Id = model.participantHeadId,
                name = model.name
            };

            await participantHeadService.SaveParticipantHead(data);

            return RedirectToAction(nameof(Index));
        }

        // POST: ParticipantHead/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}