using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers.Api
{
    /// <summary>
    /// Api controller for instructor Api services
    /// </summary>
   
    public class InstructorsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/InstructorsData
        public IQueryable<Instructor> GetPeople()
        {
            return db.Instructors;
        }

        /// <summary>
        /// This Api service is used to display an instructor weeklyschedule in response to a htttrequest: api/instructors/{id}/weeklyschedule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weeklyschedule"></param>
        /// <returns>instructor.id and his courseSessions </returns>

        // GET: api/instructors/{id}/weeklyschedule
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id, string weeklyschedule)
        {
            if (InstructorExists(id) == false)
            {
                return NotFound();
            }

            SchoolContext db = new SchoolContext();
            InstructorApiViewModel instructorApiViewModel = new InstructorApiViewModel(); //initialization of an instructorVM
            instructorApiViewModel.CourseSessionApiViewModels = new List<CourseSessionApiViewModel>(); //initialization of his List of CourseSessionVM
            List<CourseSession> courseSessions = db.CourseSessions.Where(i => i.InstructorID == id).ToList(); //recovery of the list of this instructor's CourseSessions in the db
            foreach (CourseSession courseSession in courseSessions) //foreach of this CourseSession we create a CourseSsessionVM and we add it to the list of CourseSessionVM
            {
                CourseSessionApiViewModel session = new CourseSessionApiViewModel
                {
                    CourseID = courseSession.CourseID,
                    DayOfWeek = courseSession.DayOfWeek,
                    HourStart = courseSession.HourStart,
                    Duration = ((courseSession.HourEnd - courseSession.HourStart)*60)
                };
                instructorApiViewModel.CourseSessionApiViewModels.Add(session);
            }
            return Ok(instructorApiViewModel); //return of the instructor with his list of courseSessionsVM
        }

        //method to avoid overloading the database
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //method to check if an id is an instructorId
        private bool InstructorExists(int id)
        {
            return db.Instructors.Count(e => e.ID == id) > 0;
        }
    }
}
