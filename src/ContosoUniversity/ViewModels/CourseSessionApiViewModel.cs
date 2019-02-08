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
        public int courseId { get; set; }
        public Day day { get; set; }
        public int startHour { get; set; }
        public int duration { get; set; }

    }
}

