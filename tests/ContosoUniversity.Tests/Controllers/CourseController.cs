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
    }
}
