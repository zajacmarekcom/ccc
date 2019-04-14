using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using webapp.Enums;
using webapp.Models;
using webapp.Models.ViewModels;
using webapp.Security;
using webapp.Utils.ControllersHelpers;

namespace webapp.Repository
{
    public class BusinessRepository
    {
        private readonly WebappContext _context;

        public BusinessRepository(WebappContext context)
        {
            _context = context;
        }

        public bool Exist(int? id)
        {
            return id.HasValue && _context.Businesses.Any(x => x.Id == id);
        }

        public bool NipExist(string nip)
        {
            return _context.BusinessDatas.Any(x => x.NIP == nip);
        }

        public BusinessData GetData(int? businessId)
        {
            return _context.BusinessDatas.FirstOrDefault(d => d.BusinessId == businessId);
        }

        public List<AgentBusinessData> GetWithStatus(BusinessStatus s, IPrincipal user)
        {
            var businessItems = new List<BusinessItem>();
            foreach (var b in IndexHelper.GetAllBusinesses(user, _context))
            {
                var status = 0;
                if (b.Data == null) continue;
                var name = b.Data.RecipientName;
                var visits = _context.Visits.Where(v => v.BusinessId == b.Business.Id);
                var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                if (lastAddedVisit != null)
                {
                    var step8 = _context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                    if (step8 != null)
                    {
                        status = step8.Status;
                    }
                }

                if ((BusinessStatus)status != s) continue;

                businessItems.Add(new BusinessItem
                {
                    Status = (BusinessStatus)status,
                    Name = name,
                    Id = b.Business.Id
                });
            }

            var datas = new List<AgentBusinessData>();
            var agent = _context.Users.Single(u => u.Id == ((CustomPrincipal)user).CustomIdentity.UserId);
            datas.Add(new AgentBusinessData
            {
                AgentFullName = agent.FirstName + " " + agent.LastName,
                Businesses = businessItems
            });
            return datas;
        }

        public IEnumerable<BusinessData> Search(string query, IPrincipal user)
        {
            var businesses =
                _context.Businesses.Where(b => b.CreatorId == ((CustomPrincipal) user).CustomIdentity.UserId);
            return _context.BusinessDatas.Join(businesses, d => d.BusinessId, b => b.Id, (d, b) => d).Where(
                b => (b.NIP.Replace("-", "").Contains(query.Replace("-", "")) || b.RecipientName.Contains(query))).ToList();
        }

        public IEnumerable<BusinessData> Search(string query)
        {
            var businesses =_context.Businesses;
            return _context.BusinessDatas.Join(businesses, d => d.BusinessId, b => b.Id, (d, b) => d).Where(
                b => (b.NIP.Replace("-", "").Contains(query.Replace("-", "")) || b.RecipientName.Contains(query))).ToList();
        }
    }
}