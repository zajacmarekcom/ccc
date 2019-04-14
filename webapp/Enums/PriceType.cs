using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum PriceType
    {
        [Display(Name = "Brak danych")]
        NoData = 1,

        [Display(Name = "Loco")]
        Loco = 2,

        [Display(Name = "Franco")]
        Franco = 3
    }
}
