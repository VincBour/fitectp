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
    public class StudentEnrollmentControllerTests : IntegrationTestsBase
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
        public void Create_Enrollment_Success()
        {
            Assert.True(false);
        }
        [Test]
        public void NoDuplicateCourse_Success()
        {
            Assert.True(false);
        }
        [Test]
        public void EnrollmentWithGradeNull_Success()
        {
            Assert.True(false);
        }
        [Test]
        public void EnrollmentWithoutStudentID_Fail()
        {
            Assert.True(false);
        }
        
        
    }
}
