using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum YesNo
    {
        [Display(Name = "Brak danych")]
        NoData = 1,

        [Display(Name = "Tak")]
        Yes = 2,

        [Display(Name = "Nie")]
        No = 3,

        [Display(Name = "Nie dotyczy")]
        NoApplicable = 4
    }
}