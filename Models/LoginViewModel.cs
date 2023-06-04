using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Incorrect email format!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

}