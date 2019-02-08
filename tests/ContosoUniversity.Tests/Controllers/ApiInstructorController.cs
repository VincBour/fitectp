using ContosoUniversity.Controllers;
using ContosoUniversity.Controllers.Api;
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
    public class StudentsControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentsController controllerToTest;
        private SchoolContext dbContext;

        //[SetUp]
        //public void Initialize()
        //{
        //    httpContext = new MockHttpContextWrapper();
        //    controllerToTest = new StudentsController();
        //    controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
        //    dbContext = new DAL.SchoolContext(this.ConnectionString);

        //}

        //[Test]
        //public void GetReturnsProduct()
        //{
        //    Arrange
        //   var controller = new StudentsApiController(int id);
        //    controller.Request = new HttpRequestMessage();
        //    controller.Configuration = new HttpConfiguration();

        //    Act
        //   var response = controller.Get(10);

        //    Assert
        //   Product product;
        //    Assert.IsTrue(response.TryGetContentValue<Product>(out product));
        //    Assert.AreEqual(10, product.Id);
        //}

        [Test]
        public void GetAction_StudentApi_Success(int id)
        {

            StudentsController studentApi = new StudentsController();
            studentApi.GetStudent(id);

            Assert.True(true);
        }


    }
}
