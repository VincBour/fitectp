using ContosoUniversity.BusinessClass;
using ContosoUniversity.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using ContosoUniversity.Models;
using System;
using System.Data.Entity;
using ContosoUniversity.DAL;

namespace Tests
{
    
    public class AuthenticateControllerTest
    {
        
        private AuthenticateController controllerToTest;

        public readonly Student student1 = new Student()
        {
            ID = 1,
            LastName = "Dupont",
            FirstMidName = "Jean",
            EmailAddress = "dupontj@gmail.com",
            Login = "Hulk",
            Password = "123456",
            EnrollmentDate = DateTime.Now
        };

        public readonly Student student2 = new Student()
        {
            ID = 1,
            LastName = "Dupont",
            FirstMidName = "Jean",
            EmailAddress = "dupontj@gmail.com",
            Login = "Hulk",
            Password = "123456",
            EnrollmentDate = DateTime.Now
        };

        public readonly Instructor instructor1 = new Instructor()
        {
            ID = 1,
            LastName = "Dupont",
            FirstMidName = "Jean",
            EmailAddress = "dupontj@gmail.com",
            Login = "Hulk",
            Password = "123456",
            HireDate = DateTime.Now
        };

        [TestInitialize]
        public void Init_BeforeTest()
        {
            
            controllerToTest = new AuthenticateController();

            IDatabaseInitializer<SchoolContext> db = new DropCreateDatabaseAlways<SchoolContext>();
            Database.SetInitializer(db);
            db.InitializeDatabase(new SchoolContext());
        }

        [TestCleanup]
        public void AfterTest()
        {
            controllerToTest.Dispose();
        }

        [TestMethod]
        public void StudentRegistration_NewStudent_Result_Succes()
        {
            var student = controllerToTest.CreateUser("Paul","1234","1234","student","Paulo","Edwart","paulo@gmail.com",DateTime.Now);
            
            Assert.IsTrue(student!=null);
        }

        [TestMethod]
        public void InstructorRegistration_NewInstructo_Result_Succes()
        {
            var instructor = controllerToTest.CreateUser("Paul", "1234", "1234", "student", "Paulo", "Edwart", "paulo@gmail.com", DateTime.Now);

            Assert.IsTrue(instructor != null);
        }

        [TestMethod]
        public void AccountController_LoginGet_ViewCreate()
        {
            ViewResult resultat = (ViewResult)controllerToTest.Authenticate();

            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void AccountController_RegisterGet_ViewRegister()
        {
            ViewResult resultat = (ViewResult)controllerToTest.CreateUser();

            Assert.AreEqual("", resultat.ViewName);
        }

        [TestMethod]
        public void AccountController_RegisterPostStudentAlreadyExist_ViewBag()
        {
            controllerToTest.CreateUser("Paul", "1234", "1234", "student", "Paulo", "Edwart", "paulo@gmail.com", DateTime.Now);
            ViewResult resultat = (ViewResult)controllerToTest.CreateUser("Paul", "1234", "1234", "student", "Paulo", "Edwart", "paulo@gmail.com", DateTime.Now);

            Assert.AreEqual("This login already exists.", resultat.ViewBag.MessageDoublon);
        }
    }
}
