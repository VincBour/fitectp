using System;
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

        #region Methods preexisting
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
        #endregion

        #region Authentication

        /// <summary>
        /// This httpget action is called from homepage to authentication
        /// </summary>
        /// <returns>View(Form to enter his long and password)</returns>
        [HttpGet]
        public ActionResult Authenticate()
        { 
            return View();
        }

        /// <summary>
        /// This httpost action compares the login and the password with the database to authenticate the user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>if validation: View(Home)</returns>
        [HttpPost]
        public ActionResult Authenticate(string login, string password)
        {
            if (login == null)
            {
                ViewBag.LoginNull = "Login is required";
                return View();
            }
            if (password == null)
            {
                ViewBag.PasswordNull = "Password is required";
                return View();
            }
            List<Person> person = new List<Person>();
            person = db.People.ToList();
            if (person.Exists(p => p.Login == login))
            {
                foreach (var item in person)
                {
                    if ((item.Login == login) && (item.Login == password))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.PasswordFalse = "Passworld wrong.";
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.LoginWrong = "Login not found.";
                return View();
            }
            return View();
        }
        #endregion

        #region Register a new user

        /// <summary>
        /// This httpget action is called from homepage or authenticationView to register a new user
        /// </summary>
        /// <returns>View(form to enter login and password, confirm password and specify if the new user will be student or instructor)</returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// This httppost action verify if login his available, compares the two passwords and ask the type of the user to create
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="password2"></param>
        /// <param name="SelectType"></param>
        /// <returns>if new user is student: redirect to StudentController, CreateUserAction, else if new user is instructor: redirect to InstructorController, CreateUserAction,</returns>

        [HttpPost]
        public ActionResult Register(string login, string password, string password2, string SelectType)
        {
            List<Person> person = new List<Person>();
            person = db.People.ToList();

            if (login == null)
            {
                ViewBag.LoginNull = "Login is required";
                return View();
            }
            else if (password == null)
            {
                ViewBag.PasswordNull = "Password is required";
                return View();
            }
            else if (password2 == null)
            {
                ViewBag.Password2Null = "Confirm your password";
                return View();
            }
            else if (person.Exists(p => p.Login == login))
            {
                ViewBag.LoginNotAvailable = "This login already exists.";
                return View();
            }
            else
            {
                if (password != password2)
                {
                    ViewBag.PasswordsNotEquals = "Confirmation was different from the password";
                    return View();
                }
                else
                {
                    TempData["Login"] = login;
                    TempData["Password"] = password;
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
                        ViewBag.TypeNull = "You must choose a type.";
                        return View();
                    }
                }
            }
        }
        #endregion
    }
}