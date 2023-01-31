using System;
using System.Collections.Generic;
using System.Data;
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
using OfficeOpenXml;

namespace CLUB.Areas.Bulk.Controllers
{
    [Area("Bulk")]
    [Authorize]
    public class ReceiverSMSEmailController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IGroupService groupService;
        private readonly IRlnGroupMemberService rlnGroupMemberService;
        private readonly ISMSMailService iSMSMailService;


        public ReceiverSMSEmailController(IPersonalInfoService personalInfoService, IGroupService groupService, IRlnGroupMemberService rlnGroupMemberService, ISMSMailService iSMSMailService)
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
                receivers = await groupService.GetReceiver(),
                receiverGroups = await groupService.GetReceiverGroup()
            };
            return View(model);
        }
        // GET: BulkSMSEmail
        public async Task<IActionResult> SMS()
        {
            BulkSMSViewModel model = new BulkSMSViewModel
            {
                receivers = await groupService.GetReceiver(),
                receiverGroups = await groupService.GetReceiverGroup()
            };
            return View(model);
        }
        // GET: BulkSMSEmail
        public async Task<IActionResult> Whatsapp()
        {
            BulkSMSViewModel model = new BulkSMSViewModel
            {
                receivers = await groupService.GetReceiver(),
                receiverGroups = await groupService.GetReceiverGroup()
            };
            return View(model);
        }
        // GET: BulkSMSEmail
        public async Task<IActionResult> Email()
        {
            BulkSMSViewModel model = new BulkSMSViewModel
            {
                receivers = await groupService.GetReceiver(),
                receiverGroups = await groupService.GetReceiverGroup()
            };
            return View(model);
        }
        // GET: BulkSMSEmail/
        public async Task<IActionResult> Group()
        {
            GroupViewModel model = new GroupViewModel
            {
                receiverGroups = await groupService.GetReceiverGroup()
            };
            return View(model);
        }
        // GET: BulkSMSEmail/
        public async Task<IActionResult> Receiver()
        {
            GroupViewModel model = new GroupViewModel
            {
                receivers = await groupService.GetReceiver()
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
        public async Task<IActionResult> Email ([FromForm] BulkSMSViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(model.Email == "on")
            {
                if (model.ids != null)
                {
                    foreach (var id in model.ids)
                    {

                        Receiver receiverInfo = await groupService.GetReceiverById(id);
                        if (receiverInfo.email != null)
                        {
                            if (IsValid(receiverInfo.email))
                            {
                                if (model.file == null)
                                {

                                    var email = receiverInfo.email;
                                    await iSMSMailService.SendEmailViaAppPass(email, "Opus", model.subject, model.description);
                                    //iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                                }
                                else
                                {
                                    string fileName = "";
                                    FileSave.SaveEmailFile(out fileName, model.file);
                                    //var path = "D:\Mehedi\ClubManagement\CLUB\wwwroot\EmailFiles\637742126255640548.png";
                                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", fileName);
                                    //iSMSMailService.SendEMAILWithAttatchment(empInfo.emailAddressPersonal, model.subject, model.description, path, false);
                                    await iSMSMailService.SendEmailWithAttatchments(receiverInfo.email, model.subject, path, model.description, receiverInfo.name);
                                }
                            }
                        }
                        
                    }
                }
                if (model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        
                            IEnumerable<RlnReceiverGroup> grpInfo = await rlnGroupMemberService.GetRlnReceiverGroupByGroupId(id);
                            if (grpInfo.Count() > 0)
                            {
                                foreach (var data in grpInfo)
                                {
                                if (data != null)
                                {
                                    Receiver receiverInfo = await groupService.GetReceiverById((int)data.receiverId);
                                    if (receiverInfo.email != null)
                                    {
                                        if (model.file == null)
                                        {
                                            //iSMSMailService.SendEMAIL(empInfo.emailAddressPersonal, model.subject, model.description);
                                            var email = receiverInfo.email;
                                            await iSMSMailService.SendEmailViaAppPass(email, "Opus", model.subject, model.description);
                                        }
                                        else
                                        {
                                            string fileName = "";
                                            var path = FileSave.SaveEmailFile(out fileName, model.file);
                                            //IEnumerable<string> multipath = new List<string> { "", "" };
                                            //iSMSMailService.SendEMAILWithAttatchment(empInfo.emailAddressPersonal, model.subject, model.description, path, false);
                                            await iSMSMailService.SendEmailWithAttatchments(receiverInfo.email, model.subject, path, model.description, receiverInfo.name);
                                        }
                                    }
                                }
                                }
                            }
                        
                    }
                }
            }

            return RedirectToAction(nameof(Email));

        }

        public IActionResult DownloadExcelFile()
        {
            var fileName = "Receivers.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            return PhysicalFile(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SMS ([FromForm] BulkSMSViewModel model)
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

                        Receiver receiverInfo = await groupService.GetReceiverById(id);
                        await iSMSMailService.SendSMSAsync(receiverInfo.mobile, model.description);
                        
                    }
                }

                if(model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        IEnumerable<RlnReceiverGroup> grpInfo = await rlnGroupMemberService.GetRlnReceiverGroupByGroupId(id);
                        if (grpInfo.Count() > 0)
                        {
                            foreach (var data in grpInfo)
                            {
                                if (data != null)
                                {
                                    Receiver receiverInfo = await groupService.GetReceiverById((int)data.receiverId);
                                    await iSMSMailService.SendSMSAsync(receiverInfo.mobile, model.description);
                                }
                            }
                        }
                    }
                }
            }

            return RedirectToAction(nameof(SMS));

        }
        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Whatsapp([FromForm] BulkSMSViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.Whatsapp == "on")
            {
                if (model.ids != null)
                {
                    foreach (var id in model.ids)
                    {
                        Receiver receiverInfo = await groupService.GetReceiverById(id);
                        await iSMSMailService.SendWhatsappAsync(receiverInfo.mobile, model.description);
                        
                    }
                }

                if(model.groups != null)
                {
                    foreach (var id in model.groups)
                    {
                        IEnumerable<RlnReceiverGroup> grpInfo = await rlnGroupMemberService.GetRlnReceiverGroupByGroupId(id);
                        if (grpInfo.Count() > 0)
                        {
                            foreach (var data in grpInfo)
                            {
                                if (data != null)
                                {
                                    Receiver receiverInfo = await groupService.GetReceiverById((int)data.receiverId);
                                    await iSMSMailService.SendWhatsappAsync(receiverInfo.mobile, model.description);
                                }
                            }
                        }
                    }
                }
            }

            return RedirectToAction(nameof(Whatsapp));

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

            ReceiverGroup data = new ReceiverGroup
            {
                Id = model.groupId,
                name = model.name,
            };
            var ReceiverGroupId =  await groupService.SaveReceiverGroup(data);
            
            return RedirectToAction(nameof(Group));
        }
        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Receiver([FromForm] ReceiverViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
                Receiver datas = new Receiver
                {
                    Id = model.Id,
                    name = model.name,
                    profession = model.profession,
                    company = model.company,
                    department = model.department,
                    designation = model.designation,
                    email = model.email,
                    mobile = model.mobile
                };
                await groupService.SaveReceiver(datas);
            
           
            return RedirectToAction(nameof(ReceiverList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceiverEdit([FromForm] ReceiverViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
                Receiver datas = new Receiver
                {
                    Id = model.Id,
                    name = model.name,
                    profession = model.profession,
                    company = model.company,
                    department = model.department,
                    designation = model.designation,
                    email = model.email,
                    mobile = model.mobile
                };
                await groupService.SaveReceiver(datas);
            
           
            return RedirectToAction(nameof(ReceiverList));
        }









        // POST: BulkSMSEmail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultiReceiver([FromForm] GroupViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                return View();
            }
            var sl = 1;
            foreach(var ac in model.receiverName)
            {
                Receiver datas = new Receiver
                {
                    Id = model.receiverId[sl],
                    name = model.receiverName[sl],
                    profession = model.receiverProfession[sl],
                    company = model.receiverCompany[sl],
                    department = model.receiverDepartment[sl],
                    designation = model.receiverDesignation[sl],
                    email = model.receiverEmail[sl],
                    mobile = model.receiverMobile[sl]
                };
                await groupService.SaveReceiver(datas);
                sl++;
            }
            return RedirectToAction(nameof(ReceiverUpload));
        }










        // GET: BulkSMSEmail/
        public async Task<IActionResult> DeleteReceiver(int id)
        {
            await groupService.DeleteReceiverById(id);
            return RedirectToAction(nameof(ReceiverList));
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






        public async Task<ActionResult> ReceiverList()
        {
            GroupViewModel model = new GroupViewModel
            {
                receivers = await groupService.GetReceiver()
            };
            return View(model);
        }





        public async Task<ActionResult> ReceiverUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ReceiverUpload([FromForm] IFormFile formFile)
        {
            using (var stream = formFile.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[1];

                    // Read the data from the worksheet
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var cellValue = worksheet.Cells[row, 1].Value;
                        if ((string)worksheet.Cells[row, 2].Value != null)
                        {
                            // Process the cell value
                            var data = new Receiver()
                            {
                                name = (string)worksheet.Cells[row, 2].Value,
                                designation = (string)worksheet.Cells[row, 3].Value,
                                department = (string)worksheet.Cells[row, 4].Value,
                                company = (string)worksheet.Cells[row, 5].Value,
                                profession = (string)worksheet.Cells[row, 6].Value,
                                email = (string)worksheet.Cells[row, 7].Value,
                                mobile = (string)worksheet.Cells[row, 8].Value

                            };
                            var savedata = await groupService.SaveReceiver(data);
                        }
                    }
                }
            }
            return RedirectToAction(nameof(ReceiverList));

        }
        

    }
}