using CLUB.Areas.Employee.Models;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Employee;
using CLUB.Helpers;
using CLUB.Services.Employee.Interfaces;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class InfoController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly LangGenerate<GridViewLn> _lang1;

        private readonly IPersonalInfoService personalInfoService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly ITypeService typeService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;
        private readonly IOrganizationService organizationService;
        private readonly ISpacialSkilService spacialSkilService;
        private readonly IPhotographService photographService;
        private readonly ILevelofEducationService levelofEducationService;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly IMembershipService membershipService;
        private readonly ITypeService employeeTypeService;
        private readonly ITrainingService trainingService;
        private readonly ITraningHistoryService traningHistoryService;
        private readonly IPassportInfoService passportInfoService;
        private readonly IAwardPublicationLanguageService awardPublicationService;
        private readonly IMembershipLanguageService membershipLanguageService;
        private readonly IDrivingLicenseService drivingLicenseService;

        public object dateOfBirth { get; private set; }

        public InfoController(IHostingEnvironment hostingEnvironment,
            IPassportInfoService passportInfoService,
            ITraningHistoryService traningHistoryService,
            ITrainingService trainingService,
            ITypeService employeeTypeService,
            IMembershipService membershipService,
            IServiceHistoryService serviceHistoryService, 
            IEmployeeInfoService employeeInfoService,
            ISpouseChildrenService spouseChildrenService,
            ILevelofEducationService levelofEducationService,
            IPhotographService photographService,
            IOrganizationService organizationService,
            ISubjectService subjectService, 
            IDegreeService degreeService,
            ISpacialSkilService spacialSkilService,
            IPersonalInfoService personalInfoService,
            IReligionMunicipalityService religionMunicipalityService,
            ITypeService typeService,
            UserManager<ApplicationUser> userManager,
            IDesignationDepartmentService designationDepartmentService,
            IAddressService addressService,
            IAwardPublicationLanguageService awardPublicationService, 
            IMembershipLanguageService membershipLanguageService, 
            IDrivingLicenseService drivingLicenseService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<GridViewLn>(hostingEnvironment.ContentRootPath);
            this.drivingLicenseService = drivingLicenseService;
            this.personalInfoService = personalInfoService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.designationDepartmentService = designationDepartmentService;
            this.typeService = typeService;
            this.addressService = addressService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
            this.organizationService = organizationService;
            this.spacialSkilService = spacialSkilService;
            this.photographService = photographService;
            this.levelofEducationService = levelofEducationService;
            this.spouseChildrenService = spouseChildrenService;
            this.employeeInfoService = employeeInfoService;
            this.serviceHistoryService = serviceHistoryService;
            this.membershipService = membershipService;
            this.employeeTypeService = employeeTypeService;
            this.trainingService = trainingService;
            this.traningHistoryService = traningHistoryService;
            this.passportInfoService = passportInfoService;
            this.awardPublicationService = awardPublicationService;
            this.membershipLanguageService = membershipLanguageService;
            _userManager = userManager;
        }

        // GET: Info/AllEmployeeList
        public async Task<IActionResult> AllEmployeeList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg(user.org),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                divisions = await addressService.GetAllDivision(),
                districts = await addressService.GetAllDistrict(),
                thanas = await addressService.GetAllThana(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                spacialSkills = await spacialSkilService.GetSpacialSkills(),

            };
            return View(model);
        }


        // GET: Info/AllEmployeeList
        public async Task<IActionResult> AllEmployeeListForHome()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg(user.org),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                divisions = await addressService.GetAllDivision(),
                districts = await addressService.GetAllDistrict(),
                thanas = await addressService.GetAllThana(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                spacialSkills = await spacialSkilService.GetSpacialSkills(),

            };
            return View(model);
        }

        // GET: Info/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                designations = await designationDepartmentService.GetDesignations(),
                districts = await addressService.GetAllDistrict(),
                spacialSkills = await spacialSkilService.GetSpacialSkills()
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeeInfoViewModel model)
        {
            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //int temp = await personalInfoService.IsThisEmpIDPresent(model.employeeCode);
            //bool flag = false;
            //if (temp != 0 && temp != Int32.Parse((model.employeeID)))
            //{
            //    ModelState.AddModelError(string.Empty, "This Code Already Taken");
            //    flag = true;
            //}

            //int validation = await personalInfoService.GetEmployeeInfoCheck(model.employeeCode, Int32.Parse(model.employeeID));

            if (!ModelState.IsValid )
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                model.employeeTypes = await typeService.GetAllEmployeeType();
                model.designations = await designationDepartmentService.GetDesignations();
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                
                return View(model);
            }


            DateTime dateBirth = Convert.ToDateTime(model.dateOfBirth);
            DateTime dateLPR = dateBirth.AddYears(59);
            if (model.freedomFighter == "Yes") dateLPR = dateLPR.AddYears(1);

            string ApplicationUserId = await personalInfoService.GetAuthCodeByUserId(Int32.Parse(model.employeeID));
            //Console.WriteLine("\n\n\n"+dateLPR+"\n\n\n");

            EmployeeInfo data = new EmployeeInfo
            {
                Id = Int32.Parse(model.employeeID),
                employeeCode = model.employeeCode,
                nationalID = model.nationalID,
                birthIdentificationNo = model.birthIdentificationNo,
                govtID = model.govtID,
                gpfNomineeName = model.gpfNomineeName,
                gpfAcNo = model.gpfAcNo,
                nameEnglish = model.nameEnglish,
                nameBangla = model.nameBangla,
                motherNameEnglish = model.motherNameEnglish,
                motherNameBangla = model.motherNameBangla,
                fatherNameEnglish = model.fatherNameEnglish,
                fatherNameBangla = model.fatherNameBangla,
                nationality = model.nationality,
                dateOfBirth = model.dateOfBirth,
                gender = model.gender,
                birthPlace = model.birthPlace,
                maritalStatus = model.maritalStatus,
                religionId = Convert.ToInt32(model.religion),
                employeeTypeId = Convert.ToInt32(model.employeeType),
                bloodGroup = model.bloodGroup,
                freedomFighter = model.freedomFighter == "Yes" ? true : false,
                freedomFighterNo = model.freedomFighterNo,
                telephoneOffice = model.telephoneOffice,
                telephoneResidence = model.telephoneResidence,
                emailAddress = model.emailAddress,
                mobileNumberOffice = model.mobileNumberOffice,
                mobileNumberPersonal = model.mobileNumberPersonal,
                emailAddressPersonal = model.emailAddressPersonal,
                
                natureOfRequitment = model.natureOfRequitment,
                specialSkill = model.specialSkill,
                seniorityNumber  = model.seniorityNumber,
                joiningDesignation = model.joiningDesignation,
                designation = model.designation,

                post = model.post,
                homeDistrict = model.homeDistrict,
                orgType = user.org,
                ApplicationUserId = ApplicationUserId,
                joiningDateGovtService = model.dateOfMembership,


                tShirtSize =  model.tShirtSize
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

            int lstId = await personalInfoService.SaveEmployeeInfo(data);
            return RedirectToAction(nameof(Index), new { @id = lstId });
            
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBigForm([FromForm] EmployeeInfoViewModel model)
        {
            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //int temp = await personalInfoService.IsThisEmpIDPresent(model.employeeCode);
            //bool flag = false;
            //if (temp != 0 && temp != Int32.Parse((model.employeeID)))
            //{
            //    ModelState.AddModelError(string.Empty, "This Code Already Taken");
            //    flag = true;
            //}

            //int validation = await personalInfoService.GetEmployeeInfoCheck(model.employeeCode, Int32.Parse(model.employeeID));

            if (!ModelState.IsValid /*|| validation == 0*/)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                model.employeeTypes = await typeService.GetAllEmployeeType();
                model.designations = await designationDepartmentService.GetDesignations();
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                //if (validation == 0)
                //{
                //    ModelState.AddModelError(string.Empty, "Membership Code already exists.");
                //}
                return RedirectToAction(nameof(BigForm), new { @id = model.employeeID });
            }


            DateTime dateBirth = Convert.ToDateTime(model.dateOfBirth);
            DateTime dateLPR = dateBirth.AddYears(59);
            if (model.freedomFighter == "Yes") dateLPR = dateLPR.AddYears(1);

            string ApplicationUserId = await personalInfoService.GetAuthCodeByUserId(Int32.Parse(model.employeeID));
            //Console.WriteLine("\n\n\n"+dateLPR+"\n\n\n");

            EmployeeInfo data = new EmployeeInfo
            {
                Id = Int32.Parse(model.employeeID),
                employeeCode = model.employeeCode,
                nationalID = model.nationalID,
                birthIdentificationNo = model.birthIdentificationNo,
                govtID = model.govtID,
                gpfNomineeName = model.gpfNomineeName,
                gpfAcNo = model.gpfAcNo,
                nameEnglish = model.nameEnglish,
                nameBangla = model.nameBangla,
                motherNameEnglish = model.motherNameEnglish,
                motherNameBangla = model.motherNameBangla,
                fatherNameEnglish = model.fatherNameEnglish,
                fatherNameBangla = model.fatherNameBangla,
                nationality = model.nationality,
                dateOfBirth = model.dateOfBirth,
                gender = model.gender,
                birthPlace = model.birthPlace,
                maritalStatus = model.maritalStatus,
                religionId = Convert.ToInt32(model.religion),
                employeeTypeId = Convert.ToInt32(model.employeeType),
                bloodGroup = model.bloodGroup,
                freedomFighter = model.freedomFighter == "Yes" ? true : false,
                freedomFighterNo = model.freedomFighterNo,
                telephoneOffice = model.telephoneOffice,
                telephoneResidence = model.telephoneResidence,
                emailAddress = model.emailAddress,
                mobileNumberOffice = model.mobileNumberOffice,
                mobileNumberPersonal = model.mobileNumberPersonal,
                emailAddressPersonal = model.emailAddressPersonal,

                natureOfRequitment = model.natureOfRequitment,
                specialSkill = model.specialSkill,
                seniorityNumber = model.seniorityNumber,
                joiningDesignation = model.joiningDesignation,
                designation = model.designation,

                post = model.post,
                homeDistrict = model.homeDistrict,
                orgType = user.org,
                ApplicationUserId = ApplicationUserId,
                joiningDateGovtService = model.dateOfMembership,


                tShirtSize = model.tShirtSize
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

            int lstId = await personalInfoService.SaveEmployeeInfo(data);
            return RedirectToAction(nameof(BigForm), new { @id = lstId });

        }

        // GET: Info
        public async Task<IActionResult> Index(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.employeeID = id.ToString();
            var model = new EmployeeInfoViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                designations = await designationDepartmentService.GetDesignations(),
                districts = await addressService.GetAllDistrict(),
                spacialSkills = await spacialSkilService.GetSpacialSkills()
            };
            return View(model);
        }

        public async Task<IActionResult> EditGrid(int id)
        {
            ViewBag.employeeID = id.ToString();
            PhotographViewModel model = new PhotographViewModel
            {
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                //visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            if (model.photograph == null) model.photograph = new Photograph();
            return View(model);
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GridView(int id)
        {
            ViewBag.employeeID = id.ToString();
            GridViewViewModel model = new GridViewViewModel
            {
                fLang = _lang1.PerseLang("Home/HomeEN.json", "Home/HomeBN.json", Request.Cookies["lang"]),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }












        // GET: Info/Create
        [AllowAnonymous]
        public async Task<IActionResult> BigForm(int? id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var userRoles = await _userManager.GetRolesAsync(user);
            //ViewBag.Role = userRoles.ToString();
            if (HttpContext.User.IsInRole("General Member"))
            {
                //string user = HttpContext.User.Identity.Name;
                var userinfo = await personalInfoService.GetEmployeeInfoByCode(user.UserName);
                id = userinfo.Id;
            }
            ViewBag.employeeID = id.ToString();
            //ViewBag.employeeID = id.ToString();
            var model = new InfoViewModel
            {
                
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await employeeTypeService.GetAllEmployeeType(),
                designations = await designationDepartmentService.GetDesignations(),
                districts = await addressService.GetAllDistrict(),
                spacialSkills = await spacialSkilService.GetSpacialSkills(),
                allMemberInfos = await personalInfoService.GetEmployeeInfo(),
                levelofeducationNamelist = await levelofEducationService.GetAllLevelofEducation(),
                subjects = await subjectService.GetAllSubject(),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                children = await spouseChildrenService.GetChildrenByEmpId(id),
                organizations = await organizationService.GetAllOrganization(),
                educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(id),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                addressPermanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                addressPresent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                employeeSignature = await photographService.GetPhotographByEmpIdAndType(id, "signature"),
                passportDetails = await passportInfoService.GetPassportInfoByEmpId(id),
                countries = await addressService.GetAllContry(),
                trainingCategories = await trainingService.GetTrainingCategories(),
                trainingInstitutes = await trainingService.GetTrainingInstitute(),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                awards = await awardPublicationService.GetAwardsByEmpId(id),
                publications = await awardPublicationService.GetPublicationsByEmpId(id),
                employeeLanguages = await awardPublicationService.GetLanguageByEmpId(id),
                languages = await membershipLanguageService.GetLanguageInfo(),
                licenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
            };

            return View(model);
        }






















        #region API Section
        [AllowAnonymous]
        [Route("global/api/getEmployeeInfo/")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            return Json(await personalInfoService.GetEmployeeInfo());
        }

        [AllowAnonymous]
        [Route("global/api/employeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> employeeById(string code)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json( await personalInfoService.GetEmployeeInfoByCodeAndOrg(code,user.org));
        }

        [AllowAnonymous]
        [Route("global/api/freeEmployeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> FreeEmployeeByCode(string code)
        {
            return Json(await personalInfoService.GetFreeEmployeeByCode(code));
        }
        [AllowAnonymous]
        [Route("global/api/ExistEmployeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> ExistEmployeeByCode(string code)
        {
            return Json(await personalInfoService.GetExistEmployeeByCode(code));
        }

        [AllowAnonymous]
        [Route("global/api/allEmpList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json( await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, user.org));
        }

        [AllowAnonymous]
        [Route("global/api/getEmployeeInfoAsQueryAbleMore/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoAsQueryAbleMore(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleMore(queryString, user.org));
        }


       
        #endregion

    }
}