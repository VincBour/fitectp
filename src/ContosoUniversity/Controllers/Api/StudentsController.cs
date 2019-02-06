using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers.Api
{
    public class StudentsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/Students
        public IQueryable<Student> GetPeople()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            //int i = 0;
            
            List<Enrollment> enrollments = db.Enrollments.Where(s => s.StudentID == id).ToList();
            List<string> CoursIDListe = new List<string>();
            StudentApiViewModel studentViewModel = new StudentApiViewModel();
            foreach (Enrollment enrollment in enrollments)
            {
                CoursIDListe.Add(enrollment.CourseID.ToString());
            }
            studentViewModel.ID = student.ID;
            studentViewModel.LastName = student.LastName;
            studentViewModel.FirstMidName = student.FirstMidName;
            studentViewModel.EnrollmentDate = student.EnrollmentDate;
            studentViewModel.ListCourseID = CoursIDListe;
            return Ok(studentViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}