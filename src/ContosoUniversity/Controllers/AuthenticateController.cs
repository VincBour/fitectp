using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class AuthenticateController : Controller
    {


        #region logOut
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        #endregion
    }
}