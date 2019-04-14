using System.Data.Entity;
using System.Linq;
using webapp.Models;

namespace webapp.Repository
{
    public class StepRepository
    {
        private readonly WebappContext _context;

        public StepRepository(WebappContext context)
        {
            _context = context;
        }

        public ClientStep1 GetStep1(int? visitId)
        {
            return _context.ClientSteps1.FirstOrDefault(s => s.VisitId == visitId);
        }

        public ClientStep2 GetStep2(int? visitId)
        {
            return _context.ClientSteps2.Include(s => s.LaxTypes)
                            .Include(c => c.Assortment).Include(s => s.PackedTypes).Where(dd => dd.VisitId == visitId).AsEnumerable().LastOrDefault();
        }

        public ClientStep3 GetStep3(int? visitId)
        {
            return _context.ClientSteps3.Include(s => s.Suppliers)
                .Include(s => s.Manufacturers)
                .Include(s => s.ManufacturersGroups)
                .Where(s => s.VisitId == visitId)
                .AsEnumerable()
                .LastOrDefault();
        }

        public ClientStep3_5 GetStep35(int? visitId)
        {
            return _context.ClientSteps3_5.Include(s => s.Manufacturers)
                .Include(s => s.ManufacturersGroups)
                .Include(s => s.Distributors)
                .Where(s => s.VisitId == visitId)
                .AsEnumerable()
                .LastOrDefault();
        }

        public ClientStep4 GetStep4(int? visitId)
        {
            return _context.ClientSteps4.Where(c => c.VisitId == visitId).AsEnumerable().LastOrDefault();
        }

        public ClientStep5 GetStep5(int? visitId)
        {
            return _context.ClientSteps5.Where(s => s.VisitId == visitId).AsEnumerable().LastOrDefault();
        }

        public ClientStep6 GetStep6(int? visitId)
        {
            return _context.ClientSteps6.Where(c => c.VisitId == visitId).AsEnumerable().LastOrDefault();
        }

        public ClientStep7 GetStep7(int? visitId)
        {
            return _context.ClientSteps7.Include(s => s.LaxTypes)
                        .Include(s => s.PackedTypes)
                        .SingleOrDefault(s => s.VisitId == visitId);
        }

        public ClientStep8 GetStep8(int? visitId)
        {
            return _context.ClientSteps8.Where(s => s.VisitId == visitId).AsEnumerable().LastOrDefault();
        }

        public int GetStatus(int? businessId)
        {
            var status = 0;
            var visits = _context.Visits.Where(v => v.BusinessId == businessId);
            var lastAddedVisit = visits.Where(v => v.VisitDate == visits.Max(a => a.VisitDate)).OrderByDescending(v => v.Id).FirstOrDefault();
            if (lastAddedVisit == null) return status;
            var step1 = GetStep1(lastAddedVisit.Id);
            var step2 = GetStep2(lastAddedVisit.Id);
            var step3 = GetStep3(lastAddedVisit.Id);
            var step35 = GetStep35(lastAddedVisit.Id);
            var step4 = GetStep4(lastAddedVisit.Id);
            var step5 = GetStep5(lastAddedVisit.Id);
            var step6 = GetStep6(lastAddedVisit.Id);
            var step7 = GetStep7(lastAddedVisit.Id);
            var step8 = GetStep8(lastAddedVisit.Id);
            if (step1 != null && step2 != null && step3 != null && step35 != null && step4 != null && step5 != null && step6 != null && step7 != null && step8 != null)
            {
                status = step8.Status;
            }

            return status;
        }

        public void EditStep8(ClientStep8 data)
        {
            if (data == null) return;
            var e = GetStep8(data.VisitId);
            if (e == null) return;
            if (e.Notes != null)
            {
                e.Notes.Clear();
            }
            _context.Entry(e).State = EntityState.Modified;
            _context.SaveChanges();
            e.Status = data.Status;
            e.Notes = data.Notes;
            _context.Entry(e).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddStep8(ClientStep8 data)
        {
            if (data == null) return;
            _context.ClientSteps8.Add(data);
            _context.SaveChanges();
        }
    }
}
