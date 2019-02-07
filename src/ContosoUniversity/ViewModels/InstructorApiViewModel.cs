using System.Collections.Generic;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    /// <summary>
    /// This ViewModel is used for instructor API service  
    /// One instructor can dispense a list of coursesessions
    /// </summary>

    public class InstructorApiViewModel
    {
        public int InstructorID { get; set; }

        public List<CourseSessionApiViewModel> CourseSessionApiViewModels { get; set; }

    }
}

