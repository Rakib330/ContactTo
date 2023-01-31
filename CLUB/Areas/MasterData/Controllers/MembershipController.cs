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
    public class MembershipController : Controller
    {
        private readonly LangGenerate<MembershipLn> _lang;
        private readonly IMembershipLanguageService membershipLanguageService;

        public MembershipController(IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            _lang = new LangGenerate<MembershipLn>(hostingEnvironment.ContentRootPath);
            this.membershipLanguageService = membershipLanguageService;
        }
        // GET: Membership
        public async Task<IActionResult> Index()
        {
            MembershipViewMdel model = new MembershipViewMdel
            {
                fLang = _lang.PerseLang("MasterData/MembershipEN.json", "MasterData/MembershipBN.json", Request.Cookies["lang"]),
                memberships = await membershipLanguageService.GetMembershipInfo()
            };
            return View(model);
        }

        // POST: Membership/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] MembershipViewMdel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/MembershipEN.json", "MasterData/MembershipBN.json", Request.Cookies["lang"]);
                model.memberships = await membershipLanguageService.GetMembershipInfo();
                return View(model);
            }

            Membership data = new Membership
            {
                Id = model.membershipId,
                membershipName = model.membershipName,
                membershipNameBn = model.membershipNameBn,
                membershipShortName = model.membershipShortName
            };

            await membershipLanguageService.SaveMembershipInfo(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await membershipLanguageService.DeleteMembershipById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}