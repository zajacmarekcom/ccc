using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class ReportCementsAnnualViewModel
    {
        public IEnumerable<ReportCementViewModel> Lax { get; set; }
        public IEnumerable<ReportCementViewModel> Packed { get; set; }
    }
}