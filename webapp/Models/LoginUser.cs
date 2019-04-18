using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapp.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Pole nie może być puste")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        [Required]
        public bool RememberMe { get; set; }
    }

    public class Register
    {
        public bool isTrue
        { get { return true; } }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [Remote("ValidateUserName", "Account")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa użytkownika musi mieć od 3 do 100 zaków")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Hasło musi mieć od 3 do 200 znaków")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole nie może być puste")]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessage = "Nieprawidłowy e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name="Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Imię")]
        [RegularExpression(@"^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Imię może zawierać jedynie litery")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[a-zA-ZżźćńółęąśŻŹĆĄŚĘŁÓŃ]+$", ErrorMessage = "Nazwisko może zawierać jedynie litery")]
        public string LastName { get; set; }
    }
}