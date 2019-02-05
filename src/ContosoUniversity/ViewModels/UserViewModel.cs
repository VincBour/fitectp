using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class UserViewModel
    {
        public Person Person { get; set; }
        public bool Authenticate { get; set; }
    }
}