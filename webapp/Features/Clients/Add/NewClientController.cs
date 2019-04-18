using webapp.Security;
using System.Web.Mvc;
using webapp.Models;
using webapp.Utils.ControllersHelpers;
using System;
using System.Linq;
using webapp.Features.Clients.Add;
using FluentValidation.Mvc;

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
            var validator = new NewClientViewModelValidator(_newClientDao);
            var result = validator.Validate(data);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState, null);
                data.Data = _newClientDao.GetNewClientFormData();
                return View(data);
            }

            _newClientDao.SaveNewClient(data.Model, ((CustomPrincipal)User).CustomIdentity.UserId);

            return RedirectToAction("AddVisit_Step1", "Agent", new { businessId = data.Model.BusinessId });
        }
    }
}
