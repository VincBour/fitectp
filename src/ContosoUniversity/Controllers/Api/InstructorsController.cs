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
    public class InstructorsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/InstructorsData
        public IQueryable<Instructor> GetPeople()
        {
            return db.Instructors;
        }

        // GET: api/instructors/{id}/weeklyschedule
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id, string weeklyschedule)
        {
            if (InstructorExists(id) == false)
            {
                return NotFound();
            }
            SchoolContext db = new SchoolContext();
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }
            InstructorApiViewModel instructorApiViewModel = new InstructorApiViewModel();
            instructorApiViewModel.CourseSessionApiViewModels = new List<CourseSessionApiViewModel>();
            List<CourseSession> courseSessions = db.CourseSessions.Where(i => i.InstructorID == id).ToList();
            foreach (CourseSession courseSession in courseSessions)
            {
                CourseSessionApiViewModel session = new CourseSessionApiViewModel
                {
                    CourseID = courseSession.CourseID,
                    DayOfWeek = courseSession.DayOfWeek,
                    HourStart = courseSession.HourStart,
                    Duration = (courseSession.HourEnd - courseSession.HourEnd)
                };
                instructorApiViewModel.CourseSessionApiViewModels.Add(session);
            }
            return Ok(instructorApiViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.Instructors.Count(e => e.ID == id) > 0;
        }
    }
}
