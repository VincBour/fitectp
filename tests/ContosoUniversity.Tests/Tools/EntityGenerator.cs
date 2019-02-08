using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public Instructor CreateInstructor(string lastname, string firstname)
        {
            var instructor = new Instructor()
            {
                LastName = lastname,
                FirstMidName = firstname
            };
            this.dbContext.Instructors.Add(instructor);
            return instructor;
        }

        //Méthod Encode required to test password
        private string EncodeMD5(string password)
        {
            string passwordCode = "ContoseUniversity" + password + "devnet";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordCode)));
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
                Password = EncodeMD5(password)
            };

            this.dbContext.Students.Add(student);
            this.dbContext.SaveChanges();
            return student;
        }
    }
}
