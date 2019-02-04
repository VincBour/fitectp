﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);

            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //Add ActionResult
        [HttpGet]
        public ActionResult Authenticate()
        {
            ViewBag.Message = "Authentication";

            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(string login, string password)
        {
            if (login == null)
            {
                ViewBag.LoginNull = "Login is needed";
            }
            if (password == null)
            {
                ViewBag.Password = "Password is needed";
            }
            List<Person> person = new List<Person>();
            person = db.People.ToList();
            if (person.Exists(p => p.Login == login))
            {
                foreach (var item in person)
                {
                    if ((item.Login == login) && (item.Login == password))
                    {
                        return View();
                    }
                    else
                    {
                        ViewBag.PasswordFalse = "Passworld wrong.";
                    }
                }
            }
            else
            {
                ViewBag.LoginWrong = "Login not found.";
            }
            return View(ViewBag);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Message = "Register";
            return View();
        }

        [HttpPost]
        public ActionResult Register(string login, string password, string password2, string SelectType)
        {
            if (login == null)
            {
                ViewBag.LoginNull = "Login is needed";
            }
            if (password == null)
            {
                ViewBag.PasswordNull = "Password is needed";
            }
            if (password2 == null)
            {
                ViewBag.Password2Null = "Confirm your password";
            }
            List<Person> person = new List<Person>();
            person = db.People.ToList();
            if (person.Exists(p => p.Login == login))
            {
                ViewBag.LoginNotAvailable = "This login already exists.";
            }
            if (password != password2)
            {
                ViewBag.PasswordsNotEquals = "Confirmation was different from the password";
            }
            else
            {
                ViewBag.Login = login;
                ViewBag.Password = password;
                if (SelectType == "Student")
                {
                    return RedirectToAction("CreateUser", "Student");
                }
                else if (SelectType == "Instructor")
                {
                    return RedirectToAction("CreateUser", "Instructor");
                }
                else
                {
                    return ViewBag.TypeNull = "You must choose a type.";
                }
            }
            return View(ViewBag);
        }

    }
}