using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using CLUB.Areas.Assignment.Helpers;
using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Employee.Interfaces;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    public class TransferLogController : Controller
    {
        //private readonly LangGenerate<TransferLogLn> _lang;
        private readonly LangGenerate<TransferLogLn> _lang;
        private readonly ISalaryGradeService salaryGradeService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly IPersonalInfoService personalInfoService;

        public TransferLogController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, ISalaryGradeService salaryGradeService, IServiceHistoryService serviceHistoryService)
        {
            _lang = new LangGenerate<TransferLogLn>(hostingEnvironment.ContentRootPath);
            this.salaryGradeService = salaryGradeService;
            this.serviceHistoryService = serviceHistoryService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Transfar
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TransferLogViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                salaryGrade = await salaryGradeService.GetAllSalaryGrade(),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }


        // POST: Transfar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TransferLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            TransferLog data = new TransferLog
            {
                Id = Int32.Parse(model.transfarID),
                employeeId = Int32.Parse(model.employeeID),
                workStation = model.workStation,
                from = model.fromDate,
                to = model.toDate,
                department = model.department,
                designation = model.designation
            };

            await serviceHistoryService.SaveServiceHistory(data);

            return RedirectToAction(nameof(Index));
        }

        // POST: Transfar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] TransferLogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            TransferLog data = new TransferLog
            {
                Id = Int32.Parse(model.transfarID),
                employeeId = Int32.Parse(model.employeeID),
                workStation = model.workStation,
                from = model.fromDate,
                to = model.toDate,
                department = model.department,
                designation = model.designation
            };

            await serviceHistoryService.SaveServiceHistory(data);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingService");
            //return RedirectToAction(nameof(Index));
        }



        // Delete: 
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await serviceHistoryService.DeleteServiceHistoryById(id);
            
            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: 
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await serviceHistoryService.DeleteServiceHistoryById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingService");
        }
    }
}