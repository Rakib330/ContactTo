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
    public class SpecialSkilController : Controller
    {        
        private readonly LangGenerate<SpacialSkilLn> _lang;
        private readonly ISpacialSkilService spacialSkilService;

        public SpecialSkilController(IHostingEnvironment hostingEnvironment, ISpacialSkilService spacialSkilService)
        {
            _lang = new LangGenerate<SpacialSkilLn>(hostingEnvironment.ContentRootPath);
            this.spacialSkilService = spacialSkilService;
        }

        // GET: SpacialSkil
        public async Task<IActionResult> Index()
        {
            SpacialSkilViewModel model = new SpacialSkilViewModel
            {
                fLang = _lang.PerseLang("MasterData/SpecialSkillEN.json", "MasterData/SpecialSkillBN.json", Request.Cookies["lang"]),
                spacialSkills = await spacialSkilService.GetSpacialSkills()
            };
            return View(model);
        }

        // POST: SpacialSkil/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SpacialSkilViewModel model)
        {
            int validation = await spacialSkilService.GetSpacialSkillsCheck(model.skilName, Int32.Parse(model.skilId));

            if (!ModelState.IsValid|| validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/SpecialSkillEN.json", "MasterData/SpecialSkillBN.json", Request.Cookies["lang"]);
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                ModelState.AddModelError(string.Empty, "Skill Name '"+model.skilName+"' already exists.");
                return View(model);
            }
            
            SpacialSkill data = new SpacialSkill
            {
                Id = Int32.Parse(model.skilId),
                skilName = model.skilName,
                skilNameBn = model.skilNameBn,
                shortName = model.shortName
            };

            await spacialSkilService.SaveSpacialSkill(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await spacialSkilService.DeleteSpacialSkillById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}