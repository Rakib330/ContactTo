using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
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
    public class PublicationController : Controller
    {
        private readonly LangGenerate<Publication> _lang;
        private readonly IAwardPublicationLanguageService awardPublicationService;
        private readonly IPersonalInfoService personalInfoService;

        public PublicationController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAwardPublicationLanguageService awardPublicationService)
        {
            _lang = new LangGenerate<Publication>(hostingEnvironment.ContentRootPath);
            this.awardPublicationService = awardPublicationService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Publication
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PublicationViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                publications = await awardPublicationService.GetPublicationsByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Publication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PublicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.publications = await awardPublicationService.GetPublicationsByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            Data.Entity.Employee.Publication data = new Data.Entity.Employee.Publication
            {
                Id = Int32.Parse(model.publicationId),
                employeeId = Int32.Parse(model.employeeID),
                publicationName = model.publicationName,
                publicationNubmer = model.publicationNubmer,
                publicationType = model.publicationType,
                publicationPlace = model.publicationPlace,
                publicationDate = model.publicationDate

            };

            await awardPublicationService.SavePublication(data);

            return RedirectToAction(nameof(Index));
        }

        // POST: Publication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] PublicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.publications = await awardPublicationService.GetPublicationsByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            Data.Entity.Employee.Publication data = new Data.Entity.Employee.Publication
            {
                Id = Int32.Parse(model.publicationId),
                employeeId = Int32.Parse(model.employeeID),
                publicationName = model.publicationName,
                publicationNubmer = model.publicationNubmer,
                publicationType = model.publicationType,
                publicationPlace = model.publicationPlace,
                publicationDate = model.publicationDate

            };

            await awardPublicationService.SavePublication(data);

            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingPub");
            //return RedirectToAction(nameof(Index));
        }


        // Delete: 
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await awardPublicationService.DeletePublicationById(id);

            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: 
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await awardPublicationService.DeletePublicationById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingPub");
        }

    }
}