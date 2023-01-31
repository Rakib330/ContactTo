using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    public class AwardController : Controller
    {
        private readonly LangGenerate<Award> _lang;
        private readonly IAwardPublicationLanguageService awardPublicationService;
        private readonly IPersonalInfoService personalInfoService;

        public AwardController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAwardPublicationLanguageService awardPublicationService)
        {
            _lang = new LangGenerate<Award>(hostingEnvironment.ContentRootPath);
            this.awardPublicationService = awardPublicationService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Award
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new AwardViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                awards = await awardPublicationService.GetAwardsByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Award/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AwardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.awards = await awardPublicationService.GetAwardsByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            EmployeeAward data = new EmployeeAward
            {               
                Id = Int32.Parse(model.awardId),
                employeeId = Int32.Parse(model.employeeID),
                awardName = model.awardName,
                purpose = model.perpose,
                awardDate= model.txtAwardDate

            };

            await awardPublicationService.SaveAward(data);

            return RedirectToAction(nameof(Index));
        }

        // POST: Award/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] AwardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.awards = await awardPublicationService.GetAwardsByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            EmployeeAward data = new EmployeeAward
            {
                Id = Int32.Parse(model.awardId),
                employeeId = Int32.Parse(model.employeeID),
                awardName = model.awardName,
                purpose = model.perpose,
                awardDate = model.txtAwardDate

            };

            await awardPublicationService.SaveAward(data);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingAw");
            //return RedirectToAction(nameof(Index));
        }

        // Delete: 
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await awardPublicationService.DeleteAwardById(id);

            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: 
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await awardPublicationService.DeleteAwardById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingAw");
        }
    }
}