using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapp.Models.ViewModels
{
    public class NewVisitStep1
    {
        public Visit Visit { get; set; }
        [Display(Name = "Segmenty rynku")]
        public IEnumerable<SelectedMarketSegment> MarketSegments { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
    }
}