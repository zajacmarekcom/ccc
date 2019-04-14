using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class NewVisitStep2
    {
        public Assortment Assortment { get; set; }
        public IEnumerable<SelectedLaxCementType> LaxTypes { get; set; }
        public IEnumerable<SelectedPackedCementType> PackedTypes { get; set; }
        public int VisitId { get; set; }
    }
}