﻿using ContosoUniversity.Controllers;
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
    public class FileControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private FileController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new FileController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void GetIndex_ValidfileToRetrieve_Success()
        {
           
            Assert.True(false);
        }

        
    }
}
