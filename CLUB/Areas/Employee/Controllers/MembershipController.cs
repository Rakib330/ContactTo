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
    [Authorize]
    [Area("Employee")]
    public class MembershipController : Controller
    {
        private readonly IMembershipService membershipService;
        private readonly IMembershipLanguageService membershipLanguageService;
        private readonly LangGenerate<Membership> _lang;

        public MembershipController(IMembershipService membershipService, IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            this.membershipService = membershipService;
            this.membershipLanguageService = membershipLanguageService;
            _lang = new LangGenerate<Membership>(hostingEnvironment.ContentRootPath);
        }

        // GET: Membership
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new MembershipViewModel
            {
                employeeID = id.ToString(),
                memberships = await membershipLanguageService.GetMembershipInfo(),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                fLang = _lang.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // POST: Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MembershipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.memberships = await membershipLanguageService.GetMembershipInfo();
                model.employeeMemberships = await membershipService.GetMembershipInfoByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            EmployeeMembership data = new EmployeeMembership
            {
                Id = Int32.Parse(model.membershipId),
                employeeId = Int32.Parse(model.employeeID),
                nameOrganization = model.nameOrganization,
                membershipType = model.membershipType,
                remarks = model.remarks
            };

            await membershipService.SaveMembershipInfo(data);

            return RedirectToAction(nameof(Index));
        } 
        // POST: Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexBigForm([FromForm] MembershipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.memberships = await membershipLanguageService.GetMembershipInfo();
                model.employeeMemberships = await membershipService.GetMembershipInfoByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            EmployeeMembership data = new EmployeeMembership
            {
                Id = Int32.Parse(model.membershipId),
                employeeId = Int32.Parse(model.employeeID),
                nameOrganization = model.nameOrganization,
                membershipType = model.membershipType,
                remarks = model.remarks
            };

            await membershipService.SaveMembershipInfo(data);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = model.employeeID })}#headingMembership");
            //return RedirectToAction(nameof(Index));
        }



        // Delete: Children
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await membershipService.DeleteMembershipInfoById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Membership", new
            {
                id = empId
            });
        }
        // Delete: Children
        public async Task<IActionResult> DeleteBigForm(int id, int empId)
        {
            await membershipService.DeleteMembershipInfoById(id);
            return Redirect($"{Url.RouteUrl(new { controller = "Info", action = "BigForm", area = "Employee", id = empId })}#headingMembership");
        }
    }
}