using System.ComponentModel.DataAnnotations;

namespace webapp.Enums
{
    public enum PackageType
    {
        [Display(Name = "Luz")]
        Lax = 1,

        [Display(Name = "Worek")]
        Packed = 2,

        [Display(Name = "Luz i worek")]
        Mix = 3
    }
}
