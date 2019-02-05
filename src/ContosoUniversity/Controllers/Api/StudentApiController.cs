using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContosoUniversity.Controllers.Api
{
    public class StudentApiController : ApiController
    {
        [HttpGet]
        public StudentApiViewModel Get(int? id)
        {
            SchoolContext db;
            db = new SchoolContext();

            StudentApiViewModel model = new StudentApiViewModel();
            model.Students = db.Students.Where(s => s.ID == id);

            model.Enrollments = db.Enrollments.Where(s => s.StudentID == id).ToList();

            return model;
        }
}
}