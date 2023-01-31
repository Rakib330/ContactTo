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
    public class DesignationInfoController : Controller
    {
        private readonly LangGenerate<DesignationLn> _lang;
        // GET: DesignationInfo
        private readonly IDesignationDepartmentService designationDepartmentService;

        public DesignationInfoController(IHostingEnvironment hostingEnvironment, IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<DesignationLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
        }
        // GET: DesignationInfo
        public async Task<IActionResult> Index()
        {
            DesignationInfoViewModel model = new DesignationInfoViewModel
            {
                fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]),
                designations = await designationDepartmentService.GetDesignations()
            };
            return View(model);
        }

        // POST: DesignationInfo/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DesignationInfoViewModel model)
        {
            int validation = await designationDepartmentService.GetDesignationCheck(model.designationId, model.designationName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]);
                model.designations = await designationDepartmentService.GetDesignations();
                ModelState.AddModelError(string.Empty, "Designation already exists.");
                return View(model);
            }

            Designation data = new Designation
            {
                //empType = model.empType,
                Id = model.designationId,
                designationCode = model.designationCode,
                designationName = model.designationName,
                designationNameBN = model.designationNameBn,
                shortName = model.shortName
            };

            await designationDepartmentService.SaveDesignation(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await designationDepartmentService.DeleteDesignationById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        //Get All Departmets
        [Route("global/api/Departments")]
        [HttpGet]
        public async Task<IActionResult> Department()
        {
            return Json(await designationDepartmentService.GetDepartment());
        }
        #endregion

    }
}