using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Controllers;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Features.Clients.NewClient
{
    public class NewClientDao
    {
        private readonly WebappContext _context;
        public NewClientDao(WebappContext context)
        {
            _context = context;
        }

        public void SaveNewClient(BusinessData data, int userId)
        {
            data.AddDate = DateTime.Now;
            var business = new Business { CreatorId = userId };

            _context.Businesses.Add(business);
            _context.SaveChanges();
            data.BusinessId = business.Id;
            DataController.GetPosition(ref data);
            _context.BusinessDatas.Add(data);
            _context.SaveChanges();
        }

        public AddActionAdditionalData GetNewClientFormData()
        {
            var data = new AddActionAdditionalData()
            {
                Districts = _context.Districts.ToList(),
                Provinces = _context.Provinces.ToList(),
                Agents = _context.Agents.ToList(),
                LegalForms = _context.LegalForms.ToList(),
                Groups = _context.Groups.ToList()
            };

            return data;
        }

        public bool NipExists(string nip)
        {
            return _context.BusinessDatas.Any(x => x.NIP == nip);
        }
    }
}