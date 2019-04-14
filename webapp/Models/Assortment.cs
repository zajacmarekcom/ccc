using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Assortment
    {
        public int Id { get; set; }
        public bool AnnualNone { get; set; }
        [Display(Name = "Roczne zapotrzebowanie cementu")]
        [Range(0, Int32.MaxValue, ErrorMessage="Wartość nie może być ujemna")]
        public int AnnualNeed { get; set; }
        [Display(Name = "Udział cementu w obrocie rocznym")]
        public int PartOfCementId { get; set; }
        public PartOfCement PartOfCement { get; set; }
        [Display(Name = "Rodzaj opakowania")]
        public int PackageId { get; set; }
        public KindOfPackage Package { get; set; }
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
        [Display(Name = "Uwagi dotyczące jakości")]
        public string QualityComments { get; set; }
        [Display(Name = "Siła marki")]
        public int BrandPowerId { get; set; }
    }
}