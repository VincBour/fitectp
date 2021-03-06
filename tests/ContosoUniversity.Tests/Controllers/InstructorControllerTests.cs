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
    public class InstructorControllerTests : IntegrationTestsBase
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
        public void GetDetails_ValidInstructor_Success()
        {
            //string expectedLastName = "Dubois";
            //string expectedFirstName = "George";

            //EntityGenerator generator = new EntityGenerator(dbContext);
            //Instructor instructor = generator.CreateInstructor(expectedLastName, expectedFirstName);

            //var result = controllerToTest.Details(instructor.ID) as ViewResult;
            //var resultModel = result.Model as Student;

            //Assert.That(result, Is.Not.Null);
            //Assert.That(resultModel, Is.Not.Null);
            //Assert.That(expectedLastName, Is.EqualTo(resultModel.LastName));
            //Assert.That(expectedFirstName, Is.EqualTo(resultModel.FirstMidName));
            Assert.True(false);
        }

        [Test]
        public void GetDetails_InvalidStudent_Fail404()
        {
            //const int expectedStatusCode = 404;
            //const int invalidId = 99999999;

            //var result = controllerToTest.Details(invalidId) as HttpStatusCodeResult;

            //Assert.That(result, Is.Not.Null);
            //Assert.That(expectedStatusCode, Is.EqualTo(result.StatusCode));
            Assert.True(false);
        }

        [Test]
        public void Edit_ValidStudentData_Success()
        {
            //string expectedLastName = "Wood";
            //string previousLastName = "Dubois";
            //string previousFirstName = "George";


            //EntityGenerator generator = new EntityGenerator(dbContext);
            //Student student = generator.CreateStudent(previousLastName, previousFirstName);
            //student.LastName = expectedLastName;

            //FormDataHelper.PopulateFormData(controllerToTest, student);

            ////var result = controllerToTest.EditPost(student.ID) as ViewResult;

            //Student savedStudent = dbContext.Students.Find(student.ID);

            ////Assert.That(result, Is.Not.Null);
            ////Assert.That((result.Model as Student).LastName, Is.EqualTo(expectedLastName));
            //Assert.That(savedStudent.LastName, Is.EqualTo(expectedLastName));
            Assert.True(false);
        }
    }
}
