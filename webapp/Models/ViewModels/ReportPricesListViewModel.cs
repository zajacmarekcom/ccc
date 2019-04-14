using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class ReportPricesListViewModel
    {
        public IEnumerable<ReportCementPriceViewModel> Lax { get; set; }
        public IEnumerable<ReportCementPriceViewModel> Packed { get; set; }
    }
}