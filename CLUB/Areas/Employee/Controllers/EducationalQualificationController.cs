using CLUB.Helpers;
using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CLUB.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    public class EducationalQualificationController : Controller
    {
        private readonly LangGenerate<EducationalQualificationLn> _lang;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IPersonalInfoService personalInfoService;

        public EducationalQualificationController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IEmployeeInfoService employeeInfoService)
        {
            _lang = new LangGenerate<EducationalQualificationLn>(hostingEnvironment.ContentRootPath);
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
        }

        // GET: EducationQualification
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new EducationalQualificationViewModel
            {
                employeeId =id,
                fLang = _lang.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: EducationQualification/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EducationalQualificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId;
                model.educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(model.employeeId);
                model.fLang = _lang.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            EducationalQualification data = new EducationalQualification
            {
                Id = model.educationId,
                employeeId = model.employeeId,
                institution = model.institution,
                resultId = model.resultId,
                grade = model.grade,
                passingYear = model.passingYear,
                degreeId = model.degreeId,
                organizationId = model.organizationId,
                reldegreesubjectId = model.reldegreesubjectId
            };

            await employeeInfoService.SaveEducationalQualification(data);

            return RedirectToAction(nameof(Index));

        }

        // POST: EducationQualification/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] EducationalQualificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId;
                model.educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(model.employeeId);
                model.fLang = _lang.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]);
                return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeId })}#headingTwo");
            }

            EducationalQualification data = new EducationalQualification
            {
                Id = model.educationId,
                employeeId = model.employeeId,
                institution = model.institution,
                resultId = model.resultId,
                grade = model.grade,
                passingYear = model.passingYear,
                degreeId = model.degreeId,
                organizationId = model.organizationId,
                reldegreesubjectId = model.reldegreesubjectId
            };

            await employeeInfoService.SaveEducationalQualification(data);

            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeId })}#headingTwo");

        }
        // Delete: Children
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await employeeInfoService.DeleteEducationalQualificationById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "EducationalQualification", new
            {
                id = empId
            });
        }

        #region API Section
        [Route("global/api/academicLogById/{id}")]
        [HttpGet]
        public async Task<IActionResult> academicLogById(int id)
        {
            return Json(await employeeInfoService.GetEducationalQualificationByEmpId(id));
        }
        #endregion

    }
}