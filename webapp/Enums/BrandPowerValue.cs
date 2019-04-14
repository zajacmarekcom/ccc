using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum BrandPowerValue
    {
        [Display(Name = "Nie ma znaczenia")]
        NoMatter = 1,
        [Display(Name = "Na równi z innymi")]
        Equal = 2,
        [Display(Name = "Ma przewagę")]
        Better = 3
    }
}
