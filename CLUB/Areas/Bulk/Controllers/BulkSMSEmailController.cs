using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CLUB.Areas.Bulk.Models;
using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Bulk.Interface;
using CLUB.Services.Channel.Interfaces;
using CLUB.Services.Employee.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.Bulk.Controllers
{
    [Area("Bulk")]
    [Authorize]
    public class BulkSMSEmailController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IGroupService groupService;
        private readonly IRlnGroupMemberService rlnGroupMemberService;
        private readonly ISMSMailService iSMSMailService;


        public BulkSMSEmailController(IPersonalInfoService personalInfoService, IGroupService groupService, IRlnGroupMemberService rlnGroupMemberService, ISMSMailService iSMSMailService)
        {
            this.personalInfoService = personalInfoService;
            this.groupService = groupService;
            this.rlnGroupMemberService = rlnGroupMemberService;
            this.iSMSMailService = iSMSMailService;
        }

        // GET: BulkSMSEmail
        public async Task<IActionResult> Index()
        {
            BulkSMSViewModel model = new BulkSMSViewModel
            {
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                memberGroups = await groupService.GetMemberGroup()
            };
            return View(model);
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> Group()
        {
            GroupViewModel model = new GroupViewModel
            {
                memberGroups = await groupService.GetMemberGroup()
            };
            return View(model);
        }

        // GET: BulkSMSEmail/Create
        public ActionResult CreateGroup()
        {
            return View();
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index ([FromForm] BulkSMSViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.SMS == "on")
            {
                if (model.ids != null)
                {
                    foreach (var id in model.ids)
                    {
                        
                            EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                            await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                        
                    }
                }

                if(model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        IEnumerable<RlnMemberGroup> grpInfo = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id);
                        if (grpInfo.Count() > 0)
                        {
                            foreach (var data in grpInfo)
                            {
                                if (data != null)
                                {
                                    EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById((int)data.employeeId);
                                    await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                                }
                            }
                        }
                    }
                }
                    
            }

            if(model.Email == "on")
            {
                if (model.ids != null)
                {
                    foreach (var id in model.ids)
                    {
                        
                            EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                        if (empInfo.emailAddress != null || empInfo.emailAddressPersonal != null)
                        {
                            if (IsValid(empInfo.emailAddress) || IsValid(empInfo.emailAddressPersonal))
                            {
                                if (model.file == null)
                                {

                                    var email = empInfo.emailAddress ?? empInfo.emailAddressPersonal;
                                    await iSMSMailService.SendEmailViaAppPass(email, "Bangladesh Engineers Club", model.subject, model.description);
                                    //iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                                }
                                else
                                {
                                    string fileName = "";
                                    FileSave.SaveEmailFile(out fileName, model.file);
                                    //var path = "D:\Mehedi\ClubManagement\CLUB\wwwroot\EmailFiles\637742126255640548.png";
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", fileName);
                                    //iSMSMailService.SendEMAILWithAttatchment(empInfo.emailAddressPersonal, model.subject, model.description, path, false);
                                    await iSMSMailService.SendEmailWithAttatchments(empInfo.emailAddressPersonal, model.subject, path, model.description, empInfo.nameEnglish);
                                }
                            }
                        }
                        
                    }
                }
                if (model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        
                            IEnumerable<RlnMemberGroup> grpInfo = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id);
                            if (grpInfo.Count() > 0)
                            {
                                foreach (var data in grpInfo)
                                {
                                if (data != null)
                                {
                                    EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById((int)data.employeeId);
                                    if (empInfo.emailAddress != null || empInfo.emailAddressPersonal != null)
                                    {
                                        if (model.file == null)
                                        {
                                            //iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                                            var email = empInfo.emailAddress ?? empInfo.emailAddressPersonal;
                                            await iSMSMailService.SendEmailViaAppPass(email, "Bangladesh Engineers Club", model.subject, model.description);
                                        }
                                        else
                                        {
                                            string fileName = "";
                                            var path = FileSave.SaveEmailFile(out fileName, model.file);
                                            //IEnumerable<string> multipath = new List<string> { "", "" };
                                            //iSMSMailService.SendEMAILWithAttatchment(empInfo.emailAddressPersonal, model.subject, model.description, path, false);
                                            await iSMSMailService.SendEmailWithAttatchments(empInfo.emailAddressPersonal, model.subject, path, model.description, empInfo.nameEnglish);
                                        }
                                    }
                                }
                                }
                            }
                        
                    }
                }
            }

            return RedirectToAction(nameof(Index));

        }
        
        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Group([FromForm] GroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            MemberGroup data = new MemberGroup
            {
                Id = model.groupId,
                name = model.name,
            };
            await groupService.SaveMemberGroup(data);

            return RedirectToAction(nameof(Group));
        }

        // GET: BulkSMSEmail/AddMemberInGroup/5
        public async Task<IActionResult> AddMemberInGroup(int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                groupId = id,
                employeeInfos = await rlnGroupMemberService.GetEmployeeInfoByGroupId(id),
                rlnMemberGroups = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id)
            };
            return View(model);
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemberInGroup([FromForm] GroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach(var id in model.ids)
            {
                RlnMemberGroup data = new RlnMemberGroup
                {
                    memberGroupId = model.groupId,
                    employeeId = id,
                };
                await rlnGroupMemberService.SaveRlnMemberGroup(data);
            }
            //return RedirectToAction(nameof(Group));
            return RedirectToAction("AddMemberInGroup", "BulkSMSEmail", new
            {
                id = model.groupId
            });
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> GroupDetails (int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                rlnMemberGroups = await rlnGroupMemberService.GetRlnMemberGroupByGroupId(id)
            };
            return View(model);
        }

        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupDetails([FromForm] GroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.SMS == "on")
            {
                foreach (var id in model.ids)
                {
                    EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    await iSMSMailService.SendSMSAsync(empInfo.mobileNumberPersonal, model.description);
                }
            }

            if (model.Email == "on")
            {
                foreach (var id in model.ids)
                {
                    EmployeeInfo empInfo = await personalInfoService.GetEmployeeInfoById(id);
                    if (empInfo.emailAddress != null || empInfo.emailAddressPersonal != null)
                    {
                        if (IsValid(empInfo.emailAddress) || IsValid(empInfo.emailAddressPersonal))
                        {
                            iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                        }
                    }
                }
            }

            return RedirectToAction(nameof(GroupDetails));
        }

        // GET: BulkSMSEmail/
        public async Task<IActionResult> DeleteMemberfromGroup(int id)
        {
            GroupViewModel model = new GroupViewModel
            {
                rlnMemberGroup = await rlnGroupMemberService.GetRlnMemberGroupById(id)
            };
            //return Json(model.rlnMemberGroup);
            int? groupId = model.rlnMemberGroup.memberGroupId;

            await rlnGroupMemberService.DeleteRlnMemberGroupById(id);

            return RedirectToAction("AddMemberInGroup", "BulkSMSEmail", new
            {
                id = groupId
            });
        }


        private static bool IsValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}