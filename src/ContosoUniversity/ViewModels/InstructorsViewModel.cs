using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class InstructorsViewModel
    {
        public DateTime HireDate { get; set; }
        
        public string LastName { get; set; }
        
        public string FirstMidName { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}