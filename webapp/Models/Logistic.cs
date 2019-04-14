using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Logistic
    {
        public int Id { get; set; }
        [Display(Name = "Liczba silosów")]
        public int NumberOfSilos { get; set; }
        [Display(Name = "Pojemność silosów")]
        public int CapacitySilos { get; set; }
        [Display(Name = "Pojemność magazynu cementu (zadaszony)")]
        public int CoveredMagazineCapacity { get; set; }
        [Display(Name = "Pojemność magazynu cementu (plac)")]
        public int NotCoveredMagazineCapacity { get; set; }
        [Display(Name = "Dostęp do bocznicy kolejowej czynnej")]
        public int RailwaySiding { get; set; }
        [Display(Name = "Czy odbierał cement transportem kolejowym")]
        public int? ReceiveCementByRailwaySiding { get; set; }
        [Display(Name = "Czy posiada własne cementowozy?")]
        public int OwnCementTank { get; set; }
        [Display(Name = "Liczba cementowozów")]
        public int NumberOwnCementTank { get; set; }
        [Display(Name = "Czy posiada własne samochody typu platforma?")]
        public int OwnPlatformTypeCar { get; set; }
        [Display(Name = "Liczba samochodów typu platforma")]
        public int NumberOwnPlatformTypeCar { get; set; }
        [Display(Name = "Czy posiada własne samochody typu HDS?")]
        public int OwnHDSTypeCar { get; set; }
        [Display(Name = "Liczba samochodów typu HDS")]
        public int NumberOwnHDSTypeCar { get; set; }
        [Display(Name = "Czy posiada licencję na przewóz cementu luzem w kraju?")]
        public int CountryLicense { get; set; }
        [Display(Name = "Czy posiada licencję na przewóz cementu luzem za granicą?")]
        public int AbroadLicense { get; set; }
        [Display(Name = "Luz (27 ton)")]
        public int LaxDelivery { get; set; }
        [Display(Name = "Max ilość dostawy")]
        public int MaxLaxDelivery { get; set; }
        [Display(Name = "Worek (24 tony)")]
        public int PackedDelivery { get; set; }
        [Display(Name = "Max ilość dostawy")]
        public int MaxPackedDelivery { get; set; }
    }
}