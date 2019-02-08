using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class AuthenticateControllerTest : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private AuthenticateController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new AuthenticateController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }


        //Method Encode required to test password
        private string EncodeMD5(string password)
        {
            string passwordCode = "ContoseUniversity" + password + "devnet";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordCode)));
        }
        #region Authenticate Tests

        //Arrange for the following authentication tests
        string expectedlogin = "login";
        string expectedpassword = "password";

        [SetUp] //to initialize a user to authentification test
        public void CreationOfUserToTest()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        }

        [TearDown] //to clean db after each test
        public void CleanUpDb()
        {
            SettingUpTests();
        }

        [Test]
        public void Authenticate_ValidUser_Success()
        {
            //Act
            var result = controllerToTest.Authenticate(expectedlogin, expectedpassword) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]); //Assert.AreEqual("Student", result.RouteValues["controller"]);
        }

        [Test]
        public void Authenticate_WrongPassword_AlertWrongPassword()
        {
            //Act
            ViewResult result = controllerToTest.Authenticate(expectedlogin, "wrong") as ViewResult;
            //Assert
            Assert.AreEqual("Invalid login or password.", result.ViewBag);
            Assert.AreEqual("Authenticate", result.ViewName);
        }

        [Test]
        public void Authenticate_LoginNoExist_AlertNotAvailableLogin()
        {
            //Arrange
            string noExistingLogin = "jkbrgmhb";
            //Act
            ViewResult result = controllerToTest.Authenticate(noExistingLogin, expectedpassword) as ViewResult;
            //Assert
            Assert.AreEqual("Invalid login or password.", result.ViewBag);
            Assert.AreEqual("Authenticate", result.ViewName);
        }
        #endregion

        #region CreateUser Tests

        //constant parameters to test user creation

        string lastname = "totolastname";
        string firstmidname = "totofirstmidname";
        string emailaddress = "toto@mail.com";
        DateTime hiredate = DateTime.Now;

        [Test]
        public void CreateUser_ValidStudent_Success()
        {
            //Arrange
            string login = "studentlogin";
            string password = "studentpassword";
            string passwordcrypt = EncodeMD5(password);
            string selecttype = "Student";
            //Act
            controllerToTest.CreateUser(login, password, password, selecttype, lastname, firstmidname, emailaddress, hiredate);
            //Assert
            Assert.IsNotNull(dbContext.Students.SingleOrDefault(p => p.Login == login && p.Password == passwordcrypt && p.LastName == lastname && p.FirstMidName == firstmidname));
        }

        [Test]
        public void CreateUser_ValidInstructor_Success()
        {
            //Arrange
            string login = "totologin";
            string password = "totopassword";
            string passwordcrypt = EncodeMD5(password);
            string selecttype = "Instructor";
            //Act
            controllerToTest.CreateUser(login, password, password, selecttype, lastname, firstmidname, emailaddress, hiredate);
            //Assert
            Assert.IsNotNull(dbContext.Instructors.SingleOrDefault(p => p.Login == login && p.Password == passwordcrypt && p.LastName == lastname && p.FirstMidName == firstmidname));
        }

        [Test]
        public void CreateUser_WrongConfirmPassword_Fail()
        {
            //Arrange
            string login = "student2login";
            string password = "student2password";
            string confirmpassword = "wrongpassword";
            string passwordcrypt = EncodeMD5(password);
            string selecttype = "Student";
            //Act
            controllerToTest.CreateUser(login, password, confirmpassword, selecttype, lastname, firstmidname, emailaddress, hiredate);
            //Assert
            Assert.IsNull(dbContext.Students.SingleOrDefault(p => p.Login == login && p.Password == passwordcrypt && p.LastName == lastname && p.FirstMidName == firstmidname));
        }

        [Test]
        public void CreateUser_AvailableLogin_Fail()
        {
            //Arrange
            string login = "studentlogin";
            string password = "studentpassword";
            string passwordcrypt = EncodeMD5(password);
            string selecttype = "Student";

            string password2 = "studentpassword2";
            string passwordcrypt2 = EncodeMD5(password);
            string lastname2 = "totolastname2";
            string firstmidname2 = "totofirstmidname2";
            string emailaddress2 = "toto2@mail.com";
            DateTime hiredate2 = DateTime.Now;

            //Act
            controllerToTest.CreateUser(login, password, password, selecttype, lastname, firstmidname, emailaddress, hiredate);
            var result = controllerToTest.CreateUser(login, password2, password2, selecttype, lastname2, firstmidname2, emailaddress2, hiredate2);
            //Assert
            Assert.IsNull(dbContext.Students.SingleOrDefault(p => p.Login == login && p.Password == passwordcrypt2 && p.LastName == lastname2 && p.FirstMidName == firstmidname2));
        }

        #endregion



        #region View
        [Test]
        public void AccountController_LoginGet_ViewCreate()
        {
            ViewResult resultat = (ViewResult)controllerToTest.Authenticate();

            Assert.AreEqual("", resultat.ViewName);
        }

        [Test]
        public void AccountController_RegisterGet_ViewRegister()
        {
            ViewResult resultat = (ViewResult)controllerToTest.CreateUser();

            Assert.AreEqual("", resultat.ViewName);
        }
        #endregion
    }
}