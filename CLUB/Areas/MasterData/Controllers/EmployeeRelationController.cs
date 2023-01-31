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
    public class EmployeeRelationController : Controller
    {
        private readonly LangGenerate<EmployeeRelationLn> _lang;
        // GET: EmployeeRelation
        private readonly IRelationService relationService;

        public EmployeeRelationController(IHostingEnvironment hostingEnvironment, IRelationService relationService)
        {
            _lang = new LangGenerate<EmployeeRelationLn>(hostingEnvironment.ContentRootPath);
            this.relationService = relationService;
        }
        // GET: EmployeeRelation
        public async Task<IActionResult> Index()
        {
            RelationViewModel model = new RelationViewModel
            {
                fLang = _lang.PerseLang("MasterData/RelationEN.json", "MasterData/RelationBN.json", Request.Cookies["lang"]),
                relations = await relationService.GetEmployeeRelations()
            };
            return View(model);
        }

        // POST: EmployeeRelation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RelationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/RelationEN.json", "MasterData/RelationBN.json", Request.Cookies["lang"]);
                model.relations = await relationService.GetEmployeeRelations();
                return View(model);
            }

            Relation data = new Relation
            {
                Id = model.relationId,
                relationName = model.relationName,
                relationNameBn = model.relationNameBn,
                relationShortName = model.relationShortName
            };

            await relationService.SaveEmployeeRelation(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await relationService.DeleteEmployeeRelationById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}