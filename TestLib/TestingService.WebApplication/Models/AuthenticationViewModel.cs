using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingService.WebApplication.Models
{
    public class AuthenticationViewModel
    {
        [Display(Name = "Login")]
        [Required]
        [RegularExpression(@"\w{4,20}", ErrorMessage = "Login should be from 4 to 20 characters long")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}