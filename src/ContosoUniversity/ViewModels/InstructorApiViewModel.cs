using System.Collections.Generic;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    public class InstructorApiViewModel
    {
        public int InstructorID { get; set; }

        public List<CourseSessionApiViewModel> CourseSessionApiViewModels { get; set; }

    }
}

