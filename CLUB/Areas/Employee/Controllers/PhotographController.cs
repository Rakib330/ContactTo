﻿using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    public class PhotographController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly IPhotographService photographService;
        private readonly IPersonalInfoService personalInfoService;

        public PhotographController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            this.photographService = photographService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Photograph
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.photograph == null) model.photograph = new Photograph();
            return View(model);
        }

        // POST: Photograph/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.photograph = await photographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.photograph == null) model.photograph = new Photograph();
                return View(model);
            }

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message != "success")
            {
                Errors.AddErrorToModelState("Image", message, ModelState);
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                ViewBag.employeeID = model.employeeID;
                model.photograph = await photographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.photograph == null) model.photograph = new Photograph();
                return View(model);
            }

            Photograph data = new Photograph
            {
                Id = model.photographID,
                employeeId = model.employeeID,
                url = fileName,
                type = "profile"
            };
            await photographService.SavePhotograph(data);

            //return RedirectToAction(new { @id = model.employeeID });
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "EditGrid", area = "Employee", id = model.employeeID })}");
        }

        // POST: Photograph/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IndexBigForm([FromForm] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.photograph = await photographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.photograph == null) model.photograph = new Photograph();
                return View(model);
            }

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message != "success")
            {
                Errors.AddErrorToModelState("Image", message, ModelState);
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                ViewBag.employeeID = model.employeeID;
                model.photograph = await photographService.GetPhotographByEmpIdAndType(model.employeeID, "profile");
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(model.employeeID);
                if (model.photograph == null) model.photograph = new Photograph();
                return View(model);
            }

            Photograph data = new Photograph
            {
                Id = model.photographID,
                employeeId = model.employeeID,
                url = fileName,
                type = "profile"
            };
            await photographService.SavePhotograph(data);

            //return RedirectToAction(new { @id = model.employeeID });
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}");
        }

    }
}