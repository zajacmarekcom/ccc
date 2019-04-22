using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Features.Clients.NewVisit;
using webapp.Helpers.Builders;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Controllers
{
    [Authorize(Roles = "agent")]
    public class NewVisitController : Controller
    {
        private readonly WebappContext _context;
        private readonly NewVisitDao _newVisitDao;

        public NewVisitController()
        {
            _context = new WebappContext();
            _newVisitDao = new NewVisitDao(_context);
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            if (!_newVisitDao.ClientExists(id))
            {
                return HttpNotFound();
            }

            var segments = _newVisitDao.GetSegments();
            var districts = _newVisitDao.GetDistricts();
            var provinces = _newVisitDao.GetProvinces();
            var users = _newVisitDao.GetUsers();
            var status = _newVisitDao.GetClientStatusCode(id);

            var vm = new NewVisitViewModel(id, segments, districts, provinces, users, status);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(NewVisitViewModel data)
        {
            var validator = new NewVisitViewModelValidator();
            var result = validator.Validate(data);

            if (!result.IsValid)
            {
                result.AddToModelState(ModelState, null);

                var marketSegments = _newVisitDao.GetSegments();
                var districts = _newVisitDao.GetDistricts();
                var provinces = _newVisitDao.GetProvinces();
                var users = _newVisitDao.GetUsers();

                var vm = new NewVisitViewModel(marketSegments, districts, provinces, users, data.ClientStatus, data.Model);

                return View(vm);
            }

            _newVisitDao.SaveNewVisit(data.Model);

            if (data.ClientStatus != 1)
            {
                return RedirectToAction("AddVisit_Step8", "Agent", new { currentVisit = data.Model.Visit.Id });
            }
            return RedirectToAction("AddVisit_Step2", "Agent", new { currentVisit = data.Model.Visit.Id });
        }
    }
}