using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Areas.Auth.Models;
using CLUB.Controllers;
using CLUB.Data;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using CLUB.Services.Auth.Interfaces;
using CLUB.Services.Channel.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CLUB.Services.Navbar.Interface;
using CLUB.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CLUB.Services.Employee.Interfaces;

namespace CLUB.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILoggedinUser _loggedinUser;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserInfos userInfos;
        private readonly IdbChangeHistory dbChangeService;
        private readonly INavbar navbarService;
        private readonly ISMSMailService sMSMailService;
        private readonly ApplicationDbContext _context;
        private readonly ISMSMailService iSMSMailService;
        private readonly IEmployeeInfoService employeeInfoServices;
        private readonly IPersonalInfoService personalInfoService;

        public AccountController( UserManager<ApplicationUser> userManager, ISMSMailService iSMSMailService,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, ILoggedinUser loggedinUser, IUserInfos userInfos, IdbChangeHistory dbChangeService, 
            INavbar navbarService,
            IEmployeeInfoService employeeInfoServices,
            IPersonalInfoService personalInfoService,
            ApplicationDbContext _context, ISMSMailService sMSMailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loggedinUser = loggedinUser;
            _roleManager = roleManager;
            this.userInfos = userInfos;
            this.dbChangeService = dbChangeService;
            this.navbarService = navbarService;
            this._context = _context;
            this.sMSMailService = sMSMailService;
            this.iSMSMailService = iSMSMailService;
            this.employeeInfoServices = employeeInfoServices;
            this.personalInfoService = personalInfoService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {

            string userName = HttpContext.User.Identity.Name;
            if (userName != null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SwitchedUser(UserListViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            string returnUrl = "/";
            ApplicationUser user = await _userManager.FindByNameAsync(model.userId);
            if (user != null && model.securityCode == "U.OPUS")
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ProxyUserList));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            RegisterViewModel model = new RegisterViewModel
            {
                employeeInfos = await personalInfoService.GetEmployeeInfoWithUser()
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ForgotPasswordViewModel model = new ForgotPasswordViewModel
            {
                employeeInfos = await personalInfoService.GetEmployeeInfoWithUser()
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var regiNo = RandomString(6);
                ApplicationUser user = await _userManager.FindByNameAsync(model.Name);
                user.org = regiNo;
                var result = await _userManager.UpdateAsync(user);
                if (user.Email != "")
                {
                    string mailMsg = @"<h1>Your BECL Reset Password OTP is " + regiNo + "</h1>";
                    await iSMSMailService.SendEmailViaAppPass(user.Email, "BECL", "Registration Confirmation OTP", mailMsg);
                }
                if (user.PhoneNumber != "")
                {
                    string sms = "Your BECL Reset Password OTP is " + regiNo + ". Do not Share with Anyone.";
                    await iSMSMailService.SendSMSAsync(user.PhoneNumber, sms);
                }
                return RedirectToAction("UserOTPVerifyForReset", "Account", new { Area = "Auth", userName = user.UserName });
                
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Login));
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var regiNo = RandomString(6);
                var user = new ApplicationUser { UserName = model.Name, Email = model.Email,PhoneNumber = model.Mobile, org = regiNo };
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "General Member");
                if (result.Succeeded)
                {
                    IdentityUser temp = await _userManager.FindByNameAsync(model.Name);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    if (user.Email != "")
                    {
                        string mailMsg = @"<h1>Your Registration Confirmation OTP is " + regiNo + "</h1>";
                        await iSMSMailService.SendEmailViaAppPass(user.Email, "BECL", "Registration Confirmation OTP", mailMsg);
                    }
                    if (user.PhoneNumber != "")
                    {
                        string sms = "Your BECL Registration Confirmation OTP is " + regiNo+". Do not Share with Anyone.";
                        await iSMSMailService.SendSMSAsync(user.PhoneNumber, sms);
                    }
                    return RedirectToAction("UserOTPVerify", "Account", new { Area = "Auth", userName = user.UserName });
                }
                AddErrors(result);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> OTPVarified()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            OTPCodeVarificationViewModel model = new OTPCodeVarificationViewModel
            {
                otpCode = user.org,
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OTPVarified(OtpVerification2 otp)
        {

            try
            {
                //var userName = User.Claims.FirstOrDefault().Value;
                string returnResult = "error";

                var user = await _userManager.FindByNameAsync(otp.userName);
                if (user.org == otp.otpCode)
                {
                    user.PhoneNumberConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    returnResult = "success";
                }

                return Json(returnResult);

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserOTPVerify(string userName)
        {
            var user = await userInfos.GetUserBasicInfoes(userName);
            var model = new OTPViewModel2
            {
                userInfo = user
            };
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> ResendOTPCode2(string ids)
        {
            var regiNo = RandomString(6);
            ApplicationUser user = await _userManager.FindByNameAsync(ids);
            user.org = regiNo;
            var result = await _userManager.UpdateAsync(user);
            if (user.Email != "")
            {
                string mailMsg = @"<h1>Your BECL OTP is " + regiNo + "</h1>";
                await iSMSMailService.SendEmailViaAppPass(user.Email, "BECL", "Registration Confirmation OTP", mailMsg);
            }
            if (user.PhoneNumber != "")
            {
                string sms = "Your BECL OTP is " + regiNo + ". Do not Share with Anyone.";
                await iSMSMailService.SendSMSAsync(user.PhoneNumber, sms);
            }
            //return RedirectToAction("UserOTPVerifyForReset", "Account", new { Area = "Auth", userName = user.UserName });

            return Json(true);
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserOTPVerifyForReset(string userName)
        {
            var user = await userInfos.GetUserBasicInfoes(userName);
            var model = new OTPViewModel2
            {
                userInfo = user
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePsswordViewModel model)
        {
            string message = "Fail To Update Password";
            if (ModelState.IsValid)
            {
                var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(HttpContext.User.Identity.Name), model.OldPassword, model.Password);
                message = data.ToString();
            }
            //return Json(model);
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Message = message });
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion


        public async Task<IActionResult> GetUserList(string rolename)
        {

            var data = await userInfos.GetAllUserInfosWithRegigonArea(rolename);
            return Json(data);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserList(string rolename)
        {
            
            UserListViewModel model = new UserListViewModel
            {
                aspNetUsersViewModels = await userInfos.GetAllUserInfos(),
                userRoles = await userInfos.GetAllUserRoles()
                

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserIdentityRoleCreate([FromForm] ApplicationRoleViewModel model)
        {
            var user = new ApplicationRole(model.RoleName);
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            IdentityResult result;

            if (model.RoleId == null)
            {
                result = await _roleManager.CreateAsync(user);

            }
            else
            {
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {model.RoleId} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    role.Name = model.RoleName;

                    // Update the Role using UpdateAsync
                    var result1 = await _roleManager.UpdateAsync(role);

                    if (result1.Succeeded)
                    {
                        return RedirectToAction(nameof(UserRoleCreate));
                    }

                    foreach (var error in result1.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }



            }


            return RedirectToAction(nameof(UserRoleCreate));
        }

        public async Task<IActionResult> ProxyUserList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            UserListViewModel model = new UserListViewModel
            {
                aspNetUsersViewModels = await _loggedinUser.GetAllUserInfos(),
                userRoles = await userInfos.GetAllUserRoles()
                //branches = await masterDataService.GetAllBranch(),

            };
            if (user.UserName == "0006" || User.Identity.Name == "opus")
            {
                return View(model);
            }
            else
            {
                return Redirect($"{Url.RouteUrl(new { controller = "Home", action = "MemberDashboard"})}");
            }
        }
        public async Task<IActionResult> GetProxyUserList(string rolename, string branchId)
        {
            try
            {
                var username = User.Identity.Name;
                var data = await _loggedinUser.GetAllUserInfosForProxyUser(username, rolename);
                return Json(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserRoleCreate()
        {
            var roles = _context.Roles.ToList();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)//.Where(x => x.roleNature != "SuperAdmin")
            {
                ApplicationRoleViewModel model = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(model);
            }
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel
            {
                eRPModules = await navbarService.GetAllModules(),
                roleViewModels = lstRole
            };
            return View(viewModel);
        }
        public async Task<IActionResult> DeleteRoles(string id)
        {
            try
            {
                await userInfos.DeleteRoleById(id);
                return Json("Success");
            }
            catch
            {
                return Json("Roles is Already Assigned Someone!!");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Reset(string Code)
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel
            {
                employeeInfo = await personalInfoService.GetEmployeeInfoByCode(Code)
            };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Name);
                var result = await _userManager.RemovePasswordAsync(user);
                var results = await _userManager.AddPasswordAsync(user, model.Password);
                await _userManager.UpdateAsync(user);
                string filter = model.Email;
                if (results.Succeeded)
                {
                    TempData["Success"] = "Password Changed Successfully!";
                }
                else
                {
                    AddErrors(results);
                }
            }
            //return View(model);
            return RedirectToAction(nameof(AccountController.Login));
        }



        [AllowAnonymous]
        [Route("global/api/UserByName/{code}")]
        [HttpGet]
        public async Task<IActionResult> UserByName(string code)
        {
            return Json(await _userManager.FindByNameAsync(code));
        }


    }
}