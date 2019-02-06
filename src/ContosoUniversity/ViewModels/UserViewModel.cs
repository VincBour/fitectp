using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class UserViewModel
    {
        public Person Person { get; set; }
        public bool Authenticate { get; set; }

               
        [Required]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm Passwor and Password do not Match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}