using ContosoUniversity.BusinessClass;
using ContosoUniversity.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using NUnit.Framework;
using ContosoUniversity.Models;

namespace Tests
{
    
    public class CourseSessionControllerTest
    {
        [Test]
        public void DetailView()
        {
            var controller = new CourseSessionsController();
            //var model = new CourseSession();
            var result = controller.Details(3) as ViewResult;
            var model = result.ViewData.Model;
            Assert.AreEqual(model, result.Model);
        }
    }
}
