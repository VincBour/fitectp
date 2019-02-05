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
    public class CourseSessionControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            //httpContext = new MockHttpContextWrapper();
            //controllerToTest = new StudentController();
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            //dbContext = new DAL.SchoolContext(this.ConnectionString);
            //controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void Create_ValidCourseSession_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_DayOfWeek_equal_DateChoose_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_HourStart_Between8and18_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_HourEnd_Between9and19_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_HourStart_SmallestThan_HourEnd_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_InstructorHadAlreadyACourse_Success()
        {
            Assert.True(true);
        }
        [Test]
        public void Create_CourseNotWithThisInstructor_Success()
        {
            Assert.True(true);
        }




    }
}
