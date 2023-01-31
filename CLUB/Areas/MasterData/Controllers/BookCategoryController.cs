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
using Microsoft.AspNetCore.Mvc;

namespace CLUB.Areas.MasterData.Controllers
{
    [Authorize]
    [Area("MasterData")]
    public class BookCategoryController : Controller
    {
        private readonly LangGenerate<BookCategoryLn> _lang;
        private readonly IBookAwardService bookAwardService;

        public BookCategoryController(IHostingEnvironment hostingEnvironment, IBookAwardService bookAwardService)
        {
            _lang = new LangGenerate<BookCategoryLn>(hostingEnvironment.ContentRootPath);
            this.bookAwardService = bookAwardService;
        }
        // GET: Award
        public async Task<IActionResult> Index()
        {
            BookCategoryViewModel model = new BookCategoryViewModel
            {
                fLang = _lang.PerseLang("MasterData/BookCategoryEN.json", "MasterData/BookCategoryBN.json", Request.Cookies["lang"]),
                bookCategories = await bookAwardService.GetBookCategory()
            };
            return View(model);
        }

        // POST: Award/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BookCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/BookCategoryEN.json", "MasterData/BookCategoryBN.json", Request.Cookies["lang"]);
                model.bookCategories = await bookAwardService.GetBookCategory();
                return View(model);
            }

            BookCategory data = new BookCategory
            {
                Id = Int32.Parse(model.bookCategoryId),
                bookCategoey = model.bookCategoey,
                bookCategoeyBn = model.bookCategoeyBn,
                bookCategoryShortName = model.bookCategoryShortName
            };

            await bookAwardService.SaveBookCategory(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await bookAwardService.DeleteBookCategoryById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}