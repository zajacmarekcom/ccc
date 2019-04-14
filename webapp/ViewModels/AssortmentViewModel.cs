using System.ComponentModel.DataAnnotations;
using webapp.Enums;

namespace webapp.ViewModels
{
    public class AssortmentViewModel
    {
        [Display(Name = "Roczne zapotrzebowanie na cement")]
        public string AnnualNeed { get; set; }

        [Display(Name = "Udział cementu w obrocie rocznym ( % )")]
        public string PartOfCement { get; set; }

        [Display(Name = "Rodzaj opakowania")]
        public PackageType Package { get; set; }

        [Display(Name = "Jaki procent cementu stanowi luz")]
        public int LaxCementPercent { get; set; }

        [Display(Name = "Własna marka na worku")]
        public string CustomBrand { get; set; }

        [Display(Name = "Wspólne działania marketingowe")]
        public string CommonMarketing { get; set; }
        
        [Display(Name = "Serwis doradczy")]
        public string AdvisoryService { get; set; }
        
        [Display(Name = "Świeży cement")]
        public string FreshCement { get; set; }
        
        [Display(Name = "Bezkosztowa reklamacja")]
        public string CostlessComplaint { get; set; }
        
        [Display(Name = "Inne")]
        public string Others { get; set; }
        
        [Display(Name = "Co ma wpływ na wybór dostawcy cementu")]
        public string AffectTheChoice { get; set; }
        
        [Display(Name = "Uwagi dotyczące cementu")]
        public string QualityComments { get; set; }
        
        [Display(Name = "Siła marki")]
        public BrandPowerValue BrandPower { get; set; }
    }
}
