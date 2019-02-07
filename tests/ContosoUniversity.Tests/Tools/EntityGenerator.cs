using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Tests.Tools
{
    public class EntityGenerator
    {
        private readonly SchoolContext dbContext;

        public EntityGenerator(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Student CreateStudent(string lastname, string firstname)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname
            };

            this.dbContext.Students.Add(student);
            return student;
        }
        //Used to Authentication tests
        public Student CreateStudentUser(string login, string password)
        {
            Student student = new Student()
            {
                FirstMidName = "firstmidname",
                LastName = "lastname",
                EnrollmentDate = DateTime.Now,
                EmailAddress = "email@address.com",
                Login = login,
                Password = password
            };

            this.dbContext.Students.Add(student);
            return student;
        }
    }
}
