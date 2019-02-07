using System.Collections.Generic;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;

namespace ContosoUniversity.ViewModels
{
    public class CourseSessionApiViewModel
    {
        public int CourseID { get; set; }
        public Day DayOfWeek { get; set; }
        public int HourStart { get; set; }
        public int Duration { get; set; }

    }
}

