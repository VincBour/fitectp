using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// This class allow authentication
/// We find there an auto-completed id, a login and the password and the foreign key PersonID
/// </summary>
namespace ContosoUniversity.Models
{
    public class Account
    {
        #region Attributs
        public int ID { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Login cannot be longer than 20 characters.")]
        public string Login { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Password must have 8 characters")]
        public string Password { get; set; }
        //required? à vérif
        [ForeignKey("Person")]
        public int PersonID { get; set; }

        #endregion


    }
}