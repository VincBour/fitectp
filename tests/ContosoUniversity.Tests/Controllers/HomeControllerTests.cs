using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class HomeControllerTest : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private HomeController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new HomeController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        //#region Authenticate Tests
        //[Test]
        //public void Authenticate_ValidUser_Success()
        //{
        //    //Arrange
        //    string expectedlogin = "login";
        //    string expectedpassword = "password";
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        //    //Act
        //    var result = controllerToTest.Authenticate(expectedlogin, expectedpassword) as RedirectToRouteResult;
        //    //Assert
        //    Assert.AreEqual("Index", result.RouteValues["action"]);
        //    Assert.IsNull(result.RouteValues["controller"]); //Assert.AreEqual("Student", result.RouteValues["controller"]);
        //}

        //[Test]
        //public void Authenticate_WrongPassword_AlertWrongPassword()
        //{
        //    //Arrange
        //    string expectedlogin = "login";
        //    string expectedpassword = "password";
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        //    //Act
        //    RedirectToRouteResult result = controllerToTest.Authenticate("login", "wrong") as RedirectToRouteResult;
        //    //Assert
        //    Assert.AreEqual("Authenticate", result.RouteValues["action"]);
        //    Assert.IsNull(result.RouteValues["controller"]);
        //}

        //[Test]
        //public void Authenticate_LoginOrPasswordEmpty_AlertNull()
        //{
        //    //Arrange
        //    string expectedlogin = "login";
        //    string expectedpassword = "password";
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        //    //Act
        //    RedirectToRouteResult result = controllerToTest.Authenticate(null, "password") as RedirectToRouteResult;
        //    RedirectToRouteResult result2 = controllerToTest.Authenticate("login", null) as RedirectToRouteResult;
        //    //Assert
        //    Assert.AreEqual("Authenticate", result.RouteValues["action"]);
        //    Assert.AreEqual("Authenticate", result2.RouteValues["action"]);
        //    Assert.IsNull(result.RouteValues["controller"]);
        //    Assert.IsNull(result2.RouteValues["controller"]);
        //}

        //[Test]
        //public void Authenticate_LoginNoExist_AlertNotAvailableLogin()
        //{
        //    //Arrange
        //    string expectedlogin = "login";
        //    string expectedpassword = "password";
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        //    string noExistingLogin = "jkbrgmhgxfgsdvnklbljvddklsdv";
        //    //Act
        //    RedirectToRouteResult result = controllerToTest.Authenticate(noExistingLogin, "password") as RedirectToRouteResult;
        //    //Assert
        //    Assert.AreEqual("Authenticate", result.RouteValues["action"]);
        //    Assert.IsNull(result.RouteValues["controller"]);
        //}
        //#endregion

        #region CreateUser Tests
        //        [Test]
        //        public void GetDetails_ValidStudent_Success()
        //        {
        //            //Arrange
        //            string login = "totologin";
        //            string password = "totopassword";
        //            string password2 = "totopassword";
        //            string selecttype = "Student";
        //            string lastname = "totolastname";
        //            string firstmidname = "totofirstmidname";
        //            string emailaddress = "toto@mail.com";
        //            DateTime hiredate;
        //            //Act
        //            var result = controllerToTest.CreateUser(login, password, password2, selecttype, lastname, firstmidname, emailaddress, hiredate);
        //            //Assert
        ////vérifier la présence des données dans la db

        //        }
        #endregion
    }
}