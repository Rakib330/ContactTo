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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Controllers
{
    [Authorize]
    [Area("Employee")]
    public class ChildrenController : Controller
    {
        private readonly LangGenerate<ChildrenLn> _lang;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISpacialSkilService spacialSkilService;

        public ChildrenController(IHostingEnvironment hostingEnvironment, ISpacialSkilService spacialSkilService, IPersonalInfoService personalInfoService, ISpouseChildrenService spouseChildrenService)
        {
            _lang = new LangGenerate<ChildrenLn>(hostingEnvironment.ContentRootPath);
            this.spouseChildrenService = spouseChildrenService;
            this.personalInfoService = personalInfoService;
            this.spacialSkilService = spacialSkilService;
        }

        // GET: Children
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ChildrenViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                children = await spouseChildrenService.GetChildrenByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                spacialSkills = await spacialSkilService.GetSpacialSkills()
            };
            return View(model);
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ChildrenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.children = await spouseChildrenService.GetChildrenByEmpId(Int32.Parse(model.childrenID));
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                model.fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            Children data = new Children
            {
                Id = Int32.Parse(model.childrenID),
                employeeId = Int32.Parse(model.employeeID),
                childName = model.childName,
                childNameBN = model.childNameBN,
                dateOfBirth = model.dateOfBirth,
                education = model.education,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                bloodGroup = model.bloodGroup
            };

            if (model.skills != null)
            {
                data.spacialSkillIds = String.Join(",", model.skills);
                data.spacialSkills = String.Join(", ", await spacialSkilService.GetSpacialSkillsByIds(model.skills));
            }
            else
            {
                data.spacialSkillIds = String.Empty;
                data.spacialSkills = String.Empty;
            }

            await spouseChildrenService.SaveChildren(data);

            return RedirectToAction(nameof(Index));
        }
        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] ChildrenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.children = await spouseChildrenService.GetChildrenByEmpId(Int32.Parse(model.childrenID));
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                model.fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            Children data = new Children
            {
                Id = Int32.Parse(model.childrenID),
                employeeId = Int32.Parse(model.employeeID),
                childName = model.childName,
                childNameBN = model.childNameBN,
                dateOfBirth = model.dateOfBirth,
                education = model.education,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                bloodGroup = model.bloodGroup
            };

            if (model.skills != null)
            {
                data.spacialSkillIds = String.Join(",", model.skills);
                data.spacialSkills = String.Join(", ", await spacialSkilService.GetSpacialSkillsByIds(model.skills));
            }
            else
            {
                data.spacialSkillIds = String.Empty;
                data.spacialSkills = String.Empty;
            }

            await spouseChildrenService.SaveChildren(data);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingThree");
            //return RedirectToAction(nameof(Index));
        }

        // Delete: Children
        public async Task<IActionResult> Delete(int id,int empId)
        {
            await spouseChildrenService.DeleteChildrenById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Children", new
            {
                id= empId
            });
        }
        // Delete: Children
        public async Task<IActionResult> DeleteBigForm(int id,int empId)
        {
            await spouseChildrenService.DeleteChildrenById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingThree");
        }
    }
}