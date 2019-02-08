using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class ApiStudentControllerTests : IntegrationTestsBase
    {
        //private MockHttpContextWrapper httpContext;
        //private CourseController controllerToTest;
        //private SchoolContext dbContext;

        //[SetUp]
        //public void Initialize()
        //{
        //    httpContext = new MockHttpContextWrapper();
        //    controllerToTest = new CourseController();
        //    controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
        //    dbContext = new DAL.SchoolContext(this.ConnectionString);
        //    controllerToTest.DbContext = dbContext;
        //}

        [Test]
        public void GetDetails_ValidCourse_Success()
        {
           
            Assert.True(false);
        }

        [Test]
        public void GetDetails_InvalidCourse_Fail404()
        {
            Assert.True(false);
        }

        [Test]
        public void Edit_ValidCourseData_Success()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_ValidCourseData_Success()
        {
            Assert.True(false);
        }

        [Test]
        public void GetAction_StudentApi_Success(int id)
        {

            Assert.True(false);
        }

        [Test]
        public void GetAction_FormmatStudentApi_Success(int id)
        {

            Assert.True(false);
        }
    }
}
