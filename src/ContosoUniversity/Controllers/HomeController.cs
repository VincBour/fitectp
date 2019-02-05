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
            // check login and password are completed
            if (login == string.Empty)
            {
                ViewBag.LoginNull = "Login is required";
                return View();
            }
            else if (password == string.Empty)
            {
                ViewBag.PasswordNull = "Password is required";
                return View();
            }
            //check user exists and password is correct
            else if (db.People.Any(p => p.Login == login))
            {
                Person user = db.People.SingleOrDefault(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    ViewBag.PasswordFalse = "Passworld wrong.";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginWrong = "Login not found.";
                return View();
            }
        }
        #endregion

        #region Register a new user


        /// <summary>
        /// This httpget action is called from homepage and authenticationView in order to create a new user
        /// </summary>
        /// <returns>View(form with user informations)</returns>
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// This httppost action take parameters from the form, chech their validity and add a new person to database
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="password2"></param>
        /// <param name="SelectType"></param>
        /// <param name="lastname"></param>
        /// <param name="firstmidname"></param>
        /// <param name="emailaddress"></param>
        /// <returns>add a new student or a new instructor depending of the type selected by the user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(string login, string password, string password2, string selecttype, string lastname, string firstmidname, string emailaddress, DateTime hiredate)
        {
            List<Person> person = new List<Person>();
            person = db.People.ToList();

            //Verification fields not null
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
            else if (selecttype == null)
            {
                ViewBag.TypeNull = "You must choose a type.";
                return View();
            }
            else if (lastname == null)
            {
                ViewBag.LastNameNull = "Lastname is required";
                return View();
            }
            else if (firstmidname == null)
            {
                ViewBag.FirstMidNameNull = "Firstmidname is required";
                return View();
            }
            //Check validity of login and password
            else if (person.Exists(p => p.Login == login))
            {
                ViewBag.LoginNotAvailable = "This login already exists.";
                return View();
            }
            else if (password != password2)
            {
                ViewBag.PasswordsNotEquals = "Confirmation was different from the password";
                return View();
            }
            //creation of a new user
            else
            {
                if (selecttype == "Student") //creation of a new student user
                {
                    Student student = new Student
                    {
                        FirstMidName = firstmidname,
                        LastName = lastname,
                        EnrollmentDate = DateTime.Now,
                        EmailAddress = emailaddress,
                        Login = login,
                        Password = password
                    };
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else //creation of a new instructor user
                {
                    Instructor instructor = new Instructor
                    {
                        FirstMidName = firstmidname,
                        LastName = lastname,
                        HireDate = hiredate,
                        EmailAddress = emailaddress,
                        Login = login,
                        Password = password
                    };
                    db.Instructors.Add(instructor);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        #endregion
    }
}