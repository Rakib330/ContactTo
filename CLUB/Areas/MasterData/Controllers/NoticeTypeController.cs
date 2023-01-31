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
    public class NoticeTypeController : Controller
    {
        private readonly INoticeTypeService noticeTypeService;

        public NoticeTypeController(INoticeTypeService noticeTypeService)
        {
            this.noticeTypeService = noticeTypeService;
        }

        // GET: NoticeType
        public async Task<IActionResult> Index()
        {
            NoticeTypeViewModel model = new NoticeTypeViewModel
            {
               noticeTypes = await noticeTypeService.GetAllNoticeType(),
            };
            return View(model);
        }

        // POST: SpacialSkil/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] NoticeTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            NoticeType data = new NoticeType
            {
                Id = model.TypeId,
                name = model.name,
            };

            await noticeTypeService.SaveNoticeType(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await noticeTypeService.DeleteNoticeTypeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}