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
    public class ResultController : Controller
    {
        private readonly LangGenerate<ResultInfoLn> _lang;
        private readonly IResultService resultService;

        public ResultController(IHostingEnvironment hostingEnvironment, IResultService resultService)
        {
            _lang = new LangGenerate<ResultInfoLn>(hostingEnvironment.ContentRootPath);
            this.resultService = resultService;
        }
        // GET: Result
        public async Task<IActionResult> Index()
        {
            ResultViewModel model = new ResultViewModel
            {
                fLang = _lang.PerseLang("MasterData/ResultEN.json", "MasterData/ResultBN.json", Request.Cookies["lang"]),
                resultslist = await resultService.GetAllResult()
            };
            return View(model);
        }

        // POST: Result/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ResultViewModel model)
        {
            int validation = await resultService.GetResultCheck(model.resultId,model.resultName);
            if (!ModelState.IsValid || validation == 0)
            {
                model.fLang = _lang.PerseLang("MasterData/ResultEN.json", "MasterData/ResultBN.json", Request.Cookies["lang"]);
                model.resultslist = await resultService.GetAllResult();
                ModelState.AddModelError(string.Empty, "Result Type '"+model.resultName+"' already exists.");
                return View(model);
            }

            Result data = new Result
            {
                Id = model.resultId,
                resultName = model.resultName,
                resultNameBn = model.resultNameBn,
                resultShortName = model.resultShortName,
                resultMaxValue=model.resultMaxValue
            };

            await resultService.SaveResult(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await resultService.DeleteResultById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #region API Section
        [Route("global/api/results")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Results()
        {
            return Json(await resultService.GetAllResult());
        }
        #endregion
    }
}