using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.ViewModels
{
    public class StudentApiViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Enrollment> Enrollments {get;set;}
    }
}