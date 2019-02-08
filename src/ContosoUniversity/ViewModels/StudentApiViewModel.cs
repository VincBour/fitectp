using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class StudentApiViewModel
    {

        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string enrollmentDate { get; set; }

        public List<EnrollmentViewModel> enrollments { get; set; }
    }
}