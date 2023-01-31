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
    public class DepartmentController : Controller
    {
        private readonly LangGenerate<DepartmentInfoLn> _lang;
        // GET: Department
        private readonly IDesignationDepartmentService designationDepartmentService;

        public DepartmentController(IHostingEnvironment hostingEnvironment, IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<DepartmentInfoLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            DepartmentViewModel model = new DepartmentViewModel
            {
                fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]),
                departments = await designationDepartmentService.GetDepartment()
            };
            return View(model);
        }

        // POST: Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DepartmentEN.json", "MasterData/DepartmentBN.json", Request.Cookies["lang"]);
                model.departments = await designationDepartmentService.GetDepartment();
                return View(model);
            }

            Department data = new Department
            {
                Id = Int32.Parse(model.departmentId),
                deptCode = model.deptCode,
                deptName = model.deptName,
                deptNameBn = model.deptNameBn,
                shortName = model.shortName
            };

            await designationDepartmentService.SaveDepartment(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await designationDepartmentService.DeleteDepartmentById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}