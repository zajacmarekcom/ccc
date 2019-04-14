using System.Linq;
using webapp.Models;

namespace webapp.Repository
{
    public class VisitRepository
    {
        private readonly WebappContext _context;

        public VisitRepository(WebappContext context)
        {
            _context = context;
        }

        public Visit GetVisit(int? id)
        {
            return _context.Visits.Where(v => v.Id == id).AsEnumerable().Last();
        }

        public Visit GetVisitByBusinessId(int? businessId)
        {
            return _context.Visits.Where(v => v.BusinessId == businessId).AsEnumerable().Last();
        }
    }
}