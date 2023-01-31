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
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class AwardController : Controller
    {
        private readonly LangGenerate<AwardLn> _lang;
        private readonly IBookAwardService bookAwardService;

        public AwardController(IHostingEnvironment hostingEnvironment, IBookAwardService bookAwardService)
        {
            _lang = new LangGenerate<AwardLn>(hostingEnvironment.ContentRootPath);
            this.bookAwardService = bookAwardService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            AwardViewModel model = new AwardViewModel
            {
                fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]),
                awards = await bookAwardService.GetAwardInfo()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AwardViewModel model)
        {
            int validation = await bookAwardService.GetAwardCheck(Int32.Parse(model.awardId), model.awardName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/AwardEN.json", "MasterData/AwardBN.json", Request.Cookies["lang"]);
                model.awards = await bookAwardService.GetAwardInfo();
                ModelState.AddModelError(string.Empty, "Award name '"+ model.awardName+ "' already exists.");
                return View(model);
            }

            Award data = new Award
            {
                Id = Int32.Parse(model.awardId),
                awardName = model.awardName,
                awardNameBn = model.awardNameBn,
                awardShortName = model.awardShortName
            };

            await bookAwardService.SaveAwardInfo(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await bookAwardService.DeleteAwardInfoById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }            
        }
    }
}