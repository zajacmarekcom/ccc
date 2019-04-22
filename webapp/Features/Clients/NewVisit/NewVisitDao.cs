using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Features.Clients.NewVisit
{
    public class NewVisitDao
    {
        private readonly WebappContext _context;

        public NewVisitDao(WebappContext context)
        {
            _context = context;
        }

        public bool ClientExists(int id)
        {
            return _context.Businesses.Any(x => x.Id == id);
        }

        public int GetClientStatusCode(int id)
        {
            return _context.BusinessDatas.First(x => x.BusinessId == id).Status;
        }

        public IEnumerable<MarketSegment> GetSegments()
        {
            return _context.MarketSegments.ToList();
        }

        public IEnumerable<District> GetDistricts()
        {
            return _context.Districts.ToList();
        }

        public IEnumerable<Province> GetProvinces()
        {
            return _context.Provinces.ToList();
        }

        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.Where(x => x.RoleId == 3).ToList();
        }

        public void SaveNewVisit(NewVisitStep1 model)
        {
            _context.Visits.Add(model.Visit);
            _context.SaveChanges();

            var segments = model.MarketSegments.Where(s => s.Checked).Select(s => new VisitMarketSegment
            {
                MarketSegmentId = s.Id,
                VisitId = model.Visit.Id,
                Percent = s.Percent
            }).ToList();
            _context.ClientSteps1.Add(new ClientStep1
            {
                Branches = model.Branches != null ? model.Branches.ToList() : null,
                VisitMarketSegments = segments,
                VisitId = model.Visit.Id
            });

            _context.SaveChanges();
        }
    }
}