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
    public class ParticipantsTypeController : Controller
    {
        private readonly IParticipantTypeService participantTypeService;

        public ParticipantsTypeController(IParticipantTypeService participantTypeService)
        {
            this.participantTypeService = participantTypeService;
        }

        // GET: EventCategory
        public async Task<IActionResult> Index()
        {
            ParticipantTypeViewModel model = new ParticipantTypeViewModel
            {
                participantTypes = await participantTypeService.GetAllParticipantType(),
            };
            return View(model);
        }

        // POST: EventCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ParticipantTypeViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.participantTypes = await participantTypeService.GetAllParticipantType();
                return View(model);
            }

            ParticipantType data = new ParticipantType
            {
                Id = model.participantTypeId,
                name = model.name
            };

            await participantTypeService.SaveParticipantType(data);

            return RedirectToAction(nameof(Index));
        }
    }
}