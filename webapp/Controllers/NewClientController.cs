using webapp.Security;
using System.Web.Mvc;
using webapp.Models;
using webapp.Utils.ControllersHelpers;
using System;
using System.Linq;
using webapp.Features.Clients.Add;

namespace webapp.Controllers
{
    [Authorize(Roles = "agent")]
    public class NewClientController : Controller
    {
        private readonly WebappContext _context;
        private readonly NewClientDao _newClientDao;

        public NewClientController()
        {
            _context = new WebappContext();
            _newClientDao = new NewClientDao(_context);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Data = _newClientDao.GetNewClientFormData();

            return View("~/Views/Agent/Add.cshtml");
        }

        [HttpPost]
        public ActionResult Add(BusinessData data)
        {
            var isError = !ModelState.IsValid;

            if (_newClientDao.NipExists(data.NIP))
            {
                isError = true;
                ModelState.AddModelError("NIP", "Firma o podanym numerze NIP już została dodana");
            }

            if (isError)
            {
                ViewBag.Data = _newClientDao.GetNewClientFormData();
                return View("~/Views/Agent/Add.cshtml", data);
            }

            _newClientDao.SaveNewClient(data, ((CustomPrincipal)User).CustomIdentity.UserId);

            return RedirectToAction("AddVisit_Step1", "Agent", new { businessId = data.BusinessId });
        }
    }
}
