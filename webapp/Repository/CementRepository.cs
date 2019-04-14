using System.Collections.Generic;
using System.Linq;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Repository
{
    class CementRepository
    {
        private readonly WebappContext _context;

        public CementRepository(WebappContext context)
        {
            _context = context;
        }

        public List<VisitPackedType> GetPackedCementTypes(NewVisitStep2 data)
        {
            if ((data.Assortment.PackageId == 2 || data.Assortment.PackageId == 3) && data.PackedTypes != null)
            {
                return data.PackedTypes.Where(t => t.Checked).Select(type => new VisitPackedType
                {
                    PackedTypeId = type.Id,
                    Percent = type.Percent,
                    VisitId = data.VisitId
                }).ToList();
            }
            return new List<VisitPackedType>();
        }

        public List<VisitLaxType> GetLaxCementTypes(NewVisitStep2 data)
        {
            if ((data.Assortment.PackageId == 1 || data.Assortment.PackageId == 3) && data.LaxTypes != null)
            {
                return data.LaxTypes.Where(t => t.Checked).Select(type => new VisitLaxType
                {
                    LaxTypeId = type.Id,
                    Percent = type.Percent,
                    VisitId = data.VisitId
                }).ToList();
            }
            return new List<VisitLaxType>();
        }

        public List<Producer> GetLaxProducers()
        {
            return _context.Producers.Where(producer => _context.LaxCementTypes.Count(c => c.ProducerId == producer.Id) > 0)
                    .ToList();
        }

        public List<Producer> GetPackedProducers()
        {
            return _context.Producers.Where(producer => _context.PackedCementTypes.Count(c => c.ProducerId == producer.Id) > 0)
                    .ToList();
        }
    }
}
