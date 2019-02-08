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
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new StudentController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void GetDetails_ValidCourseSession_Success()
        {
            
            Assert.True(false);
        }

        [Test]
        public void GetDetails_InvalidCourseSession_Fail404()
        {
            Assert.True(false); ;
        }

        [Test]
        public void Edit_ValidCourseSessionData_Success()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_DuplicateCourse_Failed()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_DayOfWeekAndDayStart_Same_Succes()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_HourStartBetween8And18_Succes()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_HourEndBetween9And19_Succes()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_HourEndMinusHourStartNotNegative_Succes()
        {
            Assert.True(false);
        }

       



    }
}
