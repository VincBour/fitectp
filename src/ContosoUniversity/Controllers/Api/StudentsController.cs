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
            
            //Obtain list enrollment of a student
            List<Enrollment> enrollments = db.Enrollments.Where(s => s.StudentID == id).ToList();
            //Create list of CoursID
            List<EnrollmentViewModel> CoursIDListe = new List<EnrollmentViewModel>();
            //Create a object student from studentViewModel
            StudentApiViewModel studentViewModel = new StudentApiViewModel();
            //Create a object enrollment from enrollmentViewModel
            EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel();
            //implementation of the list EnrollmentViewModel with courseId
            foreach (Enrollment enrollment in enrollments)
            {
                enrollmentViewModel.courseId = enrollment.CourseID;
                CoursIDListe.Add(enrollmentViewModel);
            }
            
            studentViewModel.id = student.ID;
            studentViewModel.lastname = student.LastName;
            studentViewModel.firstname = student.FirstMidName;
            studentViewModel.enrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd");
            studentViewModel.enrollments = CoursIDListe;
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
