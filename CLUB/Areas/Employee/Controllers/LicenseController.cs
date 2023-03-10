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
    public class LicenseController : Controller
    {
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly LangGenerate<License> _lang;

        public LicenseController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IDrivingLicenseService drivingLicenseService)
        {
            this.drivingLicenseService = drivingLicenseService;
            this.personalInfoService = personalInfoService;
            _lang = new LangGenerate<License>(hostingEnvironment.ContentRootPath);
        }

        // GET: License
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new LicenseViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LicenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            DrivingLicense data = new DrivingLicense
            {
                Id = Int32.Parse(model.licenseId),
                employeeId = Int32.Parse(model.employeeID),
                licenseNumber = model.licenseNumber,
                category = model.licenseCategory,
                placeOfIssue = model.place,
                dateOfIssue = model.dateOfIssue,
                dateOfExpair = model.dateOfExpair
            };

            await drivingLicenseService.SaveDrivingLicenseInfo(data);

            return RedirectToAction(nameof(Index));
        }


        // POST: License/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] LicenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            DrivingLicense data = new DrivingLicense
            {
                Id = Int32.Parse(model.licenseId),
                employeeId = Int32.Parse(model.employeeID),
                licenseNumber = model.licenseNumber,
                category = model.licenseCategory,
                placeOfIssue = model.place,
                dateOfIssue = model.dateOfIssue,
                dateOfExpair = model.dateOfExpair
            };

            await drivingLicenseService.SaveDrivingLicenseInfo(data);

            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingDrive");
            //return RedirectToAction(nameof(Index));
        }



        // Delete: 
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await drivingLicenseService.DeleteDrivingLicenseById(id);

            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: 
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await drivingLicenseService.DeleteDrivingLicenseById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingDrive");
        }

    }
}