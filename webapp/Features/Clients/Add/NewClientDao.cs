using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Controllers;
using webapp.Models;

namespace webapp.Features.Clients.Add
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
    }
}