using CLUB.Areas.MasterData.Models;
using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using CLUB.Helpers;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class SalaryGradeController : Controller
    {
        private readonly LangGenerate<SalaryGradeLn> _lang;
        private readonly ISalaryGradeService salaryGradeService;
        public SalaryGradeController(IHostingEnvironment hostingEnvironment, ISalaryGradeService salaryGradeService)
        {
            _lang = new LangGenerate<SalaryGradeLn>(hostingEnvironment.ContentRootPath);
            this.salaryGradeService = salaryGradeService;
        }
        public async Task<IActionResult> Index()
        {
            SalaryGradeViewModel model = new SalaryGradeViewModel
            {
                fLang = _lang.PerseLang("MasterData/SalaryGradeEN.json", "MasterData/SalaryGradeBN.json", Request.Cookies["lang"]),
                salaryGrades = await salaryGradeService.GetAllSalaryGrade()
            };
            return View(model);
        }

        // POST: SalaryGrade/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SalaryGradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/SalaryGradeEN.json", "MasterData/SalaryGradeBN.json", Request.Cookies["lang"]);
                model.salaryGrades = await salaryGradeService.GetAllSalaryGrade();
                return View(model);
            }

            SalaryGrade data = new SalaryGrade
            {
                Id = model.salaryGradeId,
                gradeName = model.gradeName,
                basicAmount = model.basicAmount,
                currentBasic = model.currentBasic,
                payScale = model.payScale,
             };

            await salaryGradeService.SaveSalaryGrade(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await salaryGradeService.DeleteSalaryGradeById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}