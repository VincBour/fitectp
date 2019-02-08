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
    public class DepartmentControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private DepartmentController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new DepartmentController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void GetDetails_ValidDepartment_Success()
        {
           
            Assert.True(false);
        }

        [Test]
        public void GetDetails_InvalidDepartment_Fail404()
        {
            Assert.True(false);
        }

        [Test]
        public void Edit_ValidDepartmentData_Success()
        {
            Assert.True(false);
        }

        [Test]
        public void Create_ValidDepartmentData_Success()
        {
            Assert.True(false);
        }
    }
}
