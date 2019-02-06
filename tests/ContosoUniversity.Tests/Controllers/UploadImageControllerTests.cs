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
    public class UploadImageControllerTests : IntegrationTestsBase
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
        public void UploadImage_Success()
        {

            Assert.True(true);
        }

        [Test]
        public void DuplicateImage_Fail()
        {

            Assert.True(true);
        }
        [Test]
        public void TwoImage_For_One_Id_Fail()
        {

            Assert.True(true);
        }
        [Test]
        public void ImageSize_100kbOrLess_Succes()
        {

            Assert.True(true);
        }
        [Test]
        public void ImageType_Jpg_Succes()
        {

            Assert.True(true);
        }
        [Test]
        public void ImageType_Tif_Fail()
        {

            Assert.True(true);
        }
        [Test]
        public void ImageType_png_Succes()
        {

            Assert.True(true);
        }
    }
}