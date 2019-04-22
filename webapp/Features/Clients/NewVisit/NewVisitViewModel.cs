using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Features.Clients.NewVisit
{
    public class NewVisitViewModel
    {
        public IEnumerable<Province> Provinces { get; }
        public IEnumerable<District> Districts { get; }
        public IEnumerable<Users> Users { get; }
        public IEnumerable<MarketSegment> MarketSegments { get; }
        public IEnumerable<SelectedMarketSegment> SelectedSegments { get; }
        public NewVisitStep1 Model { get; set; }
        public int ClientStatus { get; set; }

        public NewVisitViewModel() { }

        public NewVisitViewModel(int clientId, IEnumerable<MarketSegment> segments, IEnumerable<District> districts,
            IEnumerable<Province> provinces, IEnumerable<Users> users, int clientStatus)
        {
            Provinces = provinces;
            Districts = districts;
            MarketSegments = segments;
            SelectedSegments = segments.Select(segment => new SelectedMarketSegment { Name = segment.Name, Id = segment.Id });
            Users = users;
            Model = CreateModel(clientId);
            ClientStatus = clientStatus;
        }

        public NewVisitViewModel(IEnumerable<MarketSegment> segments, IEnumerable<District> districts,
            IEnumerable<Province> provinces, IEnumerable<Users> users, int clientStatus, NewVisitStep1 model)
        {
            Provinces = provinces;
            Districts = districts;
            MarketSegments = segments;
            SelectedSegments = segments.Select(segment => new SelectedMarketSegment { Name = segment.Name, Id = segment.Id });
            Users = users;
            Model = model;
            ClientStatus = clientStatus;
        }

        private NewVisitStep1 CreateModel(int clientId)
        {
            return new NewVisitStep1
            {
                Visit = new Visit
                {
                    VisitDate = DateTime.Now,
                    BusinessId = clientId
                }
            };
        }
    }
}