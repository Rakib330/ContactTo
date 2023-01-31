using System.Threading.Tasks;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Areas.MasterData.Models;
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
    public class LanguageController : Controller
    {
        private readonly LangGenerate<LanguageLn> _lang;
        // GET: Language
        private readonly IMembershipLanguageService membershipLanguageService;

        public LanguageController(IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            _lang = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            this.membershipLanguageService = membershipLanguageService;
        }
        // GET: Language
        public async Task<IActionResult> Index()
        {
            LanguageViewModel model = new LanguageViewModel
            {
                fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                languages = await membershipLanguageService.GetLanguageInfo()
            };
            return View(model);
        }

        // POST: Language/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LanguageViewModel model)
        {
            int validation = await membershipLanguageService.GetLanguageCheck(model.languageId, model.languageName);
            //return Json(validation);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]);
                model.languages = await membershipLanguageService.GetLanguageInfo();
                ModelState.AddModelError(string.Empty, "Language '"+ model.languageName+ "' already exists.");
                return View(model);
            }

            Language data = new Language
            {
                Id = model.languageId,
                languageName = model.languageName,
                languageNameBn = model.languageNameBn,
                languageShortName = model.languageShortName
            };

            await membershipLanguageService.SaveLanguageInfo(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await membershipLanguageService.DeleteLanguageInfoById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}