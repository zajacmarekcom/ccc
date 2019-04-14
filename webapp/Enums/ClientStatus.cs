using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum ClientStatus
    {
        [Display(Name = "")]
        Not = 0,

        [Display(Name = "Nie dotyczy")]
        NotApplicable = 1,

        [Display(Name = "Nie istnieje")]
        NotExist = 2,

        [Display(Name = "Nie jest odbiorcą cementu")]
        NotRecipient = 3
    }
}
