using webapp.Security;
using System.Web.Mvc;
using webapp.Models;
using webapp.Utils.ControllersHelpers;
using System;
using System.Linq;

namespace webapp.Controllers
{
    [Authorize(Roles = "agent")]
    public class NewClientController : Controller
    {
        private readonly WebappContext _context = new WebappContext();

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Data = AddHelper.FillAdditionalData(_context, 0);

            return View("~/Views/Agent/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(BusinessData data)
        {
            var isError = !ModelState.IsValid;

            if (_context.BusinessDatas.Any(x => x.NIP == data.NIP))
            {
                isError = true;
                ModelState.AddModelError("NIP", "Firma o podanym numerze NIP już została dodana");
            }

            if (isError)
            {
                ViewBag.Data = AddHelper.FillAdditionalData(_context, data.ProvinceId);
                return View("~/Views/Agent/Add.cshtml", data);
            }

            data.AddDate = DateTime.Now;
            var business = new Business { CreatorId = ((CustomPrincipal)User).CustomIdentity.UserId };

            _context.Businesses.Add(business);
            _context.SaveChanges();
            data.BusinessId = business.Id;
            DataController.GetPosition(ref data);
            _context.BusinessDatas.Add(data);
            _context.SaveChanges();

            return RedirectToAction("AddVisit_Step1", "Agent", new { businessId = data.BusinessId });
        }
    }
}
