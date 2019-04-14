using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapp.Enums;

namespace webapp.ViewModels
{
    public class LogisticViewModel
    {
        [Display(Name = "Liczba silosów")]
        public int NumberOfSilos { get; set; }

        [Display(Name = "Pojemność silosów")]
        public int CapacitySilos { get; set; }

        [Display(Name = "Pojemność magazynu (zadaszony)")]
        public int CoveredMagazineCapacity { get; set; }

        [Display(Name = "Pojemność magazynu cementu (plac)")]
        public int NotCoveredMagazineCapacity { get; set; }

        [Display(Name = "Dostęp do bocznicy kolejowej")]
        public YesNo RailwaySiding { get; set; }

        [Display(Name = "Czy odbierał cement transportem kolejowym")]
        public YesNo? ReceiveCementByRailwaySiding { get; set; }

        [Display(Name = "Czy posiada własne cementowozy?")]
        public YesNo OwnCementTank { get; set; }

        [Display(Name = "Liczba cementowozów")]
        public int NumberOwnCementTank { get; set; }

        [Display(Name = "Czy posiada własne samochody typu platforma")]
        public YesNo OwnPlatformTypeCar { get; set; }

        [Display(Name = "Liczba samochodów typu platforma")]
        public int NumberOwnPlatformTypeCar { get; set; }

        [Display(Name = "Czy posiada własne samochody typu HDS")]
        public YesNo OwnHdsTypeCar { get; set; }

        [Display(Name = "Liczba samochodów typu HDS")]
        public int NumberOwnHdsTypeCar { get; set; }


        public YesNo CountryLicence { get; set; }
        public YesNo AbroadLicence { get; set; }
        public YesNo LaxDelivery { get; set; }

        [Display(Name = "Maks. ilość dostawy")]
        public int MaxLaxDelivery { get; set; }
        public YesNo PackedDelivery { get; set; }

        [Display(Name = "Maks. ilość dostawy")]
        public int MaxPackedDelivery { get; set; }
        public IEnumerable<string> UnloadTypes { get; set; }
    }
}
