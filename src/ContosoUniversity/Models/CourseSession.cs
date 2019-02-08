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
        [Display(Name ="Instructor")]
        public int InstructorID { get; set; }
        [Required]
        [Display(Name = "Course")]
        public int CourseID { get; set; }
        [Required]
        [Display(Name = "Day of week")]
        public Day DayOfWeek { get; set; }
        [Required]
        [Display(Name = "Date start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        [Range(8, 18)]
        [Display(Name = "Hour start")]
        public int HourStart { get; set; }
        [Range(9, 19)]
        [Display(Name = "Hour end")]
        public int HourEnd { get; set; }
        [Required]
        public int Duration
        {
            get { return (HourEnd - HourStart)*60; } //to display duration in minutes
        }

        #endregion

        #region Navigation
        public virtual Course CourseIDNavigation { get; set; }
        public virtual Instructor InstructorIDNavigation { get; set; }
        public virtual IEnumerable<Enrollment> Enrollment { get; set; }


        #endregion
    }
}