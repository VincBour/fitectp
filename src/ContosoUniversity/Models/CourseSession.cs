using ContosoUniversity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class CourseSession
    {
        #region Constructor
        public CourseSession()
        {
            Enrollment = new HashSet<Enrollment>();
        }
        #endregion

        #region Properties
        public int ID { get; set; }
        [Required]
        public int InstructorID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public Day DayOfWeek { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        [Range(8, 18)]
        public int HourStart { get; set; }
        [Range(9, 19)]
        public int HourEnd { get; set; }
        [Required]
        public int Duration
        {
            get { return HourEnd - HourStart; }
        }

        #endregion

        #region Navigation
        public virtual Course CourseIDNavigation { get; set; }
        public virtual Instructor InstructorIDNavigation { get; set; }
        public virtual IEnumerable<Enrollment> Enrollment { get; set; }


        #endregion
    }
}