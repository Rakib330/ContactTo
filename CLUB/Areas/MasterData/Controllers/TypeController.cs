using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class TypeController : Controller
    {
        private readonly LangGenerate<EmployeeTypeLn> _lang;
        private readonly ITypeService typeService;

        public TypeController(IHostingEnvironment hostingEnvironment, ITypeService typeService)
        {
            _lang = new LangGenerate<EmployeeTypeLn>(hostingEnvironment.ContentRootPath);
            this.typeService = typeService;
        }

        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            EmployeeTypeViewModel model = new EmployeeTypeViewModel
            {
                fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType()
            };

            return View(model);
        }

       
        // POST: EmployeeType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EmployeeTypeViewModel model)
        {
            int validation = await typeService.GetEmployeeTypeCheck(model.empTypeId, model.empType);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/EmployeeTypeEN.json", "MasterData/EmployeeTypeBN.json", Request.Cookies["lang"]);
                model.employeeTypes = await typeService.GetAllEmployeeType();
                ModelState.AddModelError(string.Empty, "Membership Type '"+ model.empType +"' already exists.");
                return View(model);
            }

            EmployeeType data = new EmployeeType
            {
                Id = model.empTypeId,
                empType = model.empType,
                empTypeBn = model.empTypeBn,
                charge = model.charge,
                shortName = model.shortName
            };

            await typeService.SaveEmployeeType(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await typeService.DeleteEmployeeTypeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/membershipType")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployeeType()
        {
            return Json(await typeService.GetAllEmployeeType());
        }
        #endregion
    }
}