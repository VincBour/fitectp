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
        public string day { get; set; }
        public string startHour { get; set; }
        public string duration { get; set; }

    }
}

