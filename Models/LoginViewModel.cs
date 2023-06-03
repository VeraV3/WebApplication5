using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
  /*  public class LoginViewModel
    {
        [Required(ErrorMessage = "Unesite korisničko ime.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Unesite lozinku.")]
        public string Password { get; set; }
    }
  */
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Pasvordi se ne podudaraju")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Imejl je obavezan")]
        [EmailAddress(ErrorMessage = "Pogresan format za imejl!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pasvord je obavezno polje!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    
    }

}