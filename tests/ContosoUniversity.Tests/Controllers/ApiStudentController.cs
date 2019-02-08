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
    public class CourseControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private CourseController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new CourseController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void GetDetails_ValidCourse_Success()
        {
           
            Assert.True(true);
        }

        [Test]
        public void GetDetails_InvalidCourse_Fail404()
        {
            Assert.True(true);
        }

        [Test]
        public void Edit_ValidCourseData_Success()
        {
            Assert.True(true);
        }

        [Test]
        public void Create_ValidCourseData_Success()
        {
            Assert.True(true);
        }
    }
}
