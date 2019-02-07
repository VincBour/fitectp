
using ContosoUniversity.BusinessClass;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class AuthenticateController : Controller
    {
        private SchoolContext db = new SchoolContext();

        #region Authentication

        /// <summary>
        /// This httpget action is called from homepage to authentication
        /// </summary>
        /// <returns>View(Form to enter his long and password)</returns>
        [HttpGet]
        public ActionResult Authenticate()
        {
            UserViewModel viewModel = new UserViewModel();

            return View(viewModel);
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
            //string passwordHash = EncodeMD5(password);


            //check user exists and password is correct
            if (db.People.Any(p => p.Login == login))
            {
                Person user = db.People.SingleOrDefault(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    ViewBag.ErrorUser = ErrorMessages.AuthenticateError();
                    return View();
                }
                else
                {
                    Session["ID"] = user.ID.ToString();
                    Session["Login"] = user.Login.ToString();
                    if ((db.Students.FirstOrDefault(p => p.ID == user.ID)) != null)
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
        {
            List<Person> person = new List<Person>();
            person = db.People.ToList();

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
                    Session["Type"] = "Student";
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
                    Session["Type"] = "Instructor";
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        #endregion

        #region Encode

        private static string EncodeMD5(string password)
        {
            string passwordCode = "ContoseUniversity" + password + "devnet";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordCode)));
        }
        #endregion

        #region logOut
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}