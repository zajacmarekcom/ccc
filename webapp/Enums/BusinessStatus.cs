using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum BusinessStatus
    {
        [Display(Name = "Niedokończony")]
        Undone = 0,

        [Display(Name = "Status zielony")]
        Green = 1,

        [Display(Name = "Status żółty")]
        Yellow = 2,

        [Display(Name = "Status brązowy")]
        Brown = 3
    }
}