using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Employee.Interfaces;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly LangGenerate<LanguageLn> _lang;
        private readonly IAwardPublicationLanguageService awardPublicationService;
        private readonly  IMembershipLanguageService membershipLanguageService;
        private readonly  IPersonalInfoService personalInfoService;

        public LanguageController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IMembershipLanguageService membershipLanguageService, IAwardPublicationLanguageService awardPublicationService)
        {
            _lang = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            this.awardPublicationService = awardPublicationService;
            this.membershipLanguageService = membershipLanguageService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Language
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new LanguageViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                employeeLanguages = await awardPublicationService.GetLanguageByEmpId(id),
                languages =  await membershipLanguageService.GetLanguageInfo(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }


        // POST: Language/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LanguageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.employeeLanguages = await awardPublicationService.GetLanguageByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]);
                model.languages = await membershipLanguageService.GetLanguageInfo();
                return View(model);
            }

            EmployeeLanguage data = new EmployeeLanguage
            {
                Id = Int32.Parse(model.languageId),
                employeeId = Int32.Parse(model.employeeID),
                reading = model.reading,
                writing = model.writing,
                speaking = model.speaking,
                languageId = Int32.Parse(model.language),
                proficiency = model.proficiency

            };

            await awardPublicationService.SaveLanguage(data);

            return RedirectToAction(nameof(Index));
        }


        // POST: Language/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] LanguageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.employeeLanguages = await awardPublicationService.GetLanguageByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]);
                model.languages = await membershipLanguageService.GetLanguageInfo();
                return View(model);
            }

            EmployeeLanguage data = new EmployeeLanguage
            {
                Id = Int32.Parse(model.languageId),
                employeeId = Int32.Parse(model.employeeID),
                reading = model.reading,
                writing = model.writing,
                speaking = model.speaking,
                languageId = Int32.Parse(model.language),
                proficiency = model.proficiency

            };

            await awardPublicationService.SaveLanguage(data);

            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingLang");
            //return RedirectToAction(nameof(Index));
        }

        // Delete: 
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await awardPublicationService.DeleteLanguageById(id);

            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: 
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await awardPublicationService.DeleteLanguageById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingLang");
        }


    }
}