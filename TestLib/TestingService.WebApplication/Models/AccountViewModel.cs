using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingService.WebApplication.Models
{
    public class AccountViewModel
    {
        [Display(Name = "Login")]
        [Required]
        //[Remote("DoesLoginExist", "Account", HttpMethod = "POST", ErrorMessage = "Login already exists. Please enter a different login.")]
        [RegularExpression(@"\w{4,20}", ErrorMessage = "Login should be from 4 to 20 alphanumeric characters long")]
        public string Login { get; set; }

        [EmailAddress]
        //[Remote("DoesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        [Display(Name = "Email address")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Second name")]
        [Required]
        public string SecondName { get; set; }

        [Display(Name = "Third name")]
        [Required]
        public string ThirdName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}