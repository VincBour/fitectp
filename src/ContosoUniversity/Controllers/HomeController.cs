using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    [AllowAnonymous]
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
        #region User
        public Person obtainPerson(int id)
        {
            return db.People.FirstOrDefault(p => p.ID == id);
        }

        public Person obtainPerson(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return obtainPerson(id);
            }
            return null;
        }
        public Person checkUser(string login, string password)
        {
            string passwordEncode = EncodeMD5(password);
            return db.People.FirstOrDefault(p => p.Login == login && p.Password == passwordEncode);
        }
        #region Encode

        private string EncodeMD5(string password)
        {
            string passwordCode = "ContoseUniversity" + password + "devnet";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordCode)));
        }
        #endregion
        #endregion

        #region Authentication

        /// <summary>
        /// This httpget action is called from homepage to authentication
        /// </summary>
        /// <returns>View(Form to enter his long and password)</returns>
        [HttpGet]
        public ActionResult Authenticate()
        {
            UserViewModel viewModel = new UserViewModel { Authenticate = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Person = obtainPerson(HttpContext.User.Identity.Name);
            }
            return View(viewModel);
        }

        /// <summary>
        /// This httpost action compares the login and the password with the database to authenticate the user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>if validation: View(Home)</returns>
        [HttpPost]
        public ActionResult Authenticate(string login, string password/*UserViewModel viewModel, string returnUrl*/)
        {
            string passwordHash = EncodeMD5(password);
            //// check login and password are completed
            //if (login == string.Empty)
            //{
            //    ViewBag.LoginNull = "Login is required";
            //    return View();
            //}
            //else if (password == string.Empty)
            //{
            //    ViewBag.PasswordNull = "Password is required";
            //    return View();
            //}

            //check user exists and password is correct
            if (db.People.Any(p => p.Login == login))
            {
                Person user = db.People.SingleOrDefault(u => u.Login == login && u.Password == passwordHash);
                if (user == null)
                {
                    ViewBag.PasswordFalse = "Password wrong.";
                    return View();
                }
                else
                {
                    Session["ID"] = user.ID.ToString();
                    Session["Login"] = user.Login.ToString();
                    if ((db.Students.FirstOrDefault(p=>p.ID==user.ID))!=null)
                    {
                        Session["Type"] = "Student";
                    }
                    else
                    {
                        Session["Type"] = "Instructor";
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginWrong = "Login not found.";
                return View();
            }
            //return View();
            //if (ModelState.IsValid)
            //{
            //    Person person = checkUser(viewModel.Person.Login, viewModel.Person.Password);
            //    if (person != null)
            //    {
            //        FormsAuthentication.SetAuthCookie(person.ID.ToString(), false);
            //        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            //        {
            //            return Redirect(returnUrl);
            //        }
            //        return Redirect("/");
            //    }
            //    ModelState.AddModelError("User", "Login or password wrong");
            //}
            //return View(viewModel);
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
        public ActionResult CreateUser(string login, string password, string confirmPassword, string selecttype, string lastname, string firstmidname, string emailaddress, DateTime hiredate)
        // public ActionResult CreateUser([Bind(Include = "LastName,FirstMidName,Type,Login,Password,ConfirmPassword,HireDate,EmailAdress")]RegisterViewModel viewModel)
        {
            List<Person> person = new List<Person>();
            person = db.People.ToList();
            //if (ModelState.IsValid)
            //{
            //    if (viewModel.Type.ToString() == "Instructor")
            //    {
            //        Instructor instructor = new Instructor
            //        {
            //            FirstMidName = viewModel.FirstMidName,
            //            LastName = viewModel.LastName,
            //            HireDate = viewModel.HireDate,
            //            EmailAddress = viewModel.EmailAddress,
            //            Login = viewModel.Login,
            //            Password = viewModel.Password


            //        };

            //        db.Instructors.Add(instructor);
            //        db.SaveChanges();
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        Student student = new Student();

            //        student.FirstMidName = viewModel.FirstMidName;
            //        student.LastName = viewModel.LastName;
            //        student.EnrollmentDate = DateTime.Now;
            //        student.EmailAddress = viewModel.EmailAddress;
            //        student.Login = viewModel.Login;
            //        student.Password = viewModel.Password;

            //        db.Students.Add(student);
            //        db.SaveChanges();
            //        return RedirectToAction("Index", "Home");
            //    }


            //}
            //return View(viewModel);

            //Verification fields not null
            //if (login == null)
            //{
            //    ViewBag.LoginNull = "Login is required";
            //    return View();
            //}
            //else if (password == null)
            //{
            //    ViewBag.PasswordNull = "Password is required";
            //    return View();
            //}
            //else
            if (confirmPassword == null)
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
            //else if (password != confirmPassword)
            //{
            //    ViewBag.PasswordsNotEquals = "Confirmation was different from the password";
            //    return View();
            //}
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
                        Password = EncodeMD5(password)
                    };
                    db.Students.Add(student);
                    db.SaveChanges();
                    Session["ID"] = student.ID.ToString();
                    Session["Login"] = student.Login.ToString();
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
                        Password = EncodeMD5(password)
                    };
                    db.Instructors.Add(instructor);
                    db.SaveChanges();
                    Session["ID"] = instructor.ID.ToString();
                    Session["Login"] = instructor.Login.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
        }

    }
    #endregion
    



}
