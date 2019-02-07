using System.Collections.Generic;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    /// <summary>
    /// This ViewModel is used for instructor Api service which returns the courseId, DayOfWeek, Hour and Duration of the course session
    /// </summary>
   
    public class CourseSessionApiViewModel
    {
        public int CourseID { get; set; }
        public Day DayOfWeek { get; set; }
        public int HourStart { get; set; }
        public int Duration { get; set; }

    }
}

