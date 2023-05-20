using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unesite korisničko ime.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Unesite lozinku.")]
        public string Password { get; set; }
    }
}