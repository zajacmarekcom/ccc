using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels
{
    public class BranchViewModel
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Województwo")]
        public string Province { get; set; }

        [Display(Name = "Powiat")]
        public string District { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Numer budynku")]
        public string BuildingNumber { get; set; }

        [Display(Name = "Numer telefonu")]
        public string Phone { get; set; }

        [Display(Name = "Uwagi")]
        public string Comments { get; set; }
    }
}
