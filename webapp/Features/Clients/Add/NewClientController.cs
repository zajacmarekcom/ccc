using webapp.Security;
using System.Web.Mvc;
using webapp.Models;
using webapp.Utils.ControllersHelpers;
using System;
using System.Linq;
using webapp.Features.Clients.Add;

namespace webapp.Features.Clients.Add
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
            var vm = new NewClientViewModel
            {
                Data = _newClientDao.GetNewClientFormData()
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(NewClientViewModel data)
        {
            var isError = !ModelState.IsValid;

            if (_newClientDao.NipExists(data.Model.NIP))
            {
                isError = true;
                ModelState.AddModelError("Model.NIP", "Firma o podanym numerze NIP już została dodana");
            }

            if (isError)
            {
                data.Data = _newClientDao.GetNewClientFormData();
                return View(data);
            }

            _newClientDao.SaveNewClient(data.Model, ((CustomPrincipal)User).CustomIdentity.UserId);

            return RedirectToAction("AddVisit_Step1", "Agent", new { businessId = data.Model.BusinessId });
        }
    }
}
