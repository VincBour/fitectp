using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class HomeControllerTest : IntegrationTestsBase
    {
        private SchoolContext db = new SchoolContext();

        #region Authenticate Tests
        [Test]
        public void Authenticate_ValidUser_Success()
        {
            Student student = new Student
            {
                FirstMidName = "totoname",
                LastName = "totolastname",
                EmailAddress = "toto",
                Login = "totologin",
                Password = "totopassword"
            };
            db.Students.Add(student);
            HomeController controller = new HomeController();
            var result = controller.Authenticate("totologin", "totopassword");
            //Assert.AreEqual("Index", result); good
        }

        [Test]
        public void Authenticate_WrongPassword_AlertWrongPassword()
        {
            Student student = new Student
            {
                FirstMidName = "totoname",
                LastName = "totolastname",
                EmailAddress = "toto",
                Login = "totologin",
                Password = "totopassword"
            };
            db.Students.Add(student);
            HomeController controller = new HomeController();
            var result = controller.Authenticate("totologin", "false");
            //Assert.AreEqual("Index", result); notgood
        }

        [Test]
        public void Authenticate_LoginOrPasswordEmpty_AlertNull()
        {
            Student student = new Student
            {
                FirstMidName = "totoname",
                LastName = "totolastname",
                EmailAddress = "toto",
                Login = "totologin",
                Password = "totopassword"
            };
            db.Students.Add(student);

            HomeController controller = new HomeController();
            ViewResult result_loginNull = controller.Authenticate(null, "totopassword") as ViewResult;
            var result_passwordNull = controller.Authenticate("totologin", null);
            //Assert.IsNotNull(result_loginNull);
            //Assert() notgood
        }

        [Test]
        public void Authenticate_LoginNoExist_AlertNotAvailableLogin()
        {
            Student student = new Student
            {
                FirstMidName = "totoname",
                LastName = "totolastname",
                EmailAddress = "toto",
                Login = "totologin",
                Password = "totopassword"
            };
            db.Students.Add(student);
            HomeController controller = new HomeController();
            var result = controller.Authenticate("kjnfsghbyirgrnlkfjqijfjlskjfbjkdnfsjqkfbqlvl", "totopassword");
            // Assert notgood;
        }
        #endregion

        #region CreateUser Tests
        [Test]
        public void GetDetails_ValidStudent_Success()
        {
            string login = "totologin";
            string password = "totopassword";
            string password2 = "totopassword";
            string selecttype = "Student";
            string lastname = "totolastname";
            string firstmidname = "totofirstmidname";
            string emailaddress = "toto@mail.com";
            DateTime hiredate;
            HomeController controller = new HomeController();
            var result = controller.CreateUser(login, password, password2, selecttype, lastname, firstmidname, emailaddress, hiredate);
            {

                Assert.AreEqual(db.Students.result, "totologin"));
                Assert.That(resultModel, Is.Not.Null);
                Assert.That(expectedLastName, Is.EqualTo(resultModel.LastName));
                Assert.That(expectedFirstName, Is.EqualTo(resultModel.FirstMidName));
            }
        }
        #endregion
    }
}