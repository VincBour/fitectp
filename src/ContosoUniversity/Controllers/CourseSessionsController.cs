using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.BusinessClass;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class CourseSessionsController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }
        // GET: CourseSessions
        public ActionResult Index()
        {
            var courseSessions = db.CourseSessions.Include(c => c.CourseIDNavigation).Include(c => c.InstructorIDNavigation);
            return View(courseSessions.ToList());
        }

        // GET: CourseSessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSession courseSession = db.CourseSessions.Find(id);
            if (courseSession == null)
            {
                return HttpNotFound();
            }
            return View(courseSession);
        }

        // GET: CourseSessions/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName");
            return View();
        }

        // POST: CourseSessions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InstructorID,CourseID,DayOfWeek,DateTime,HourStart,HourEnd")] CourseSession courseSession)
        {
            if (ModelState.IsValid)
            {
                List<CourseSession> sessions = new List<CourseSession>();
                sessions = db.CourseSessions.ToList();
                foreach (var item in sessions)
                {
                    if (courseSession.InstructorID == item.InstructorID && courseSession.CourseID == item.CourseID)
                    {
                        ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
                        ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName");
                        ViewBag.ErrorMessage = ErrorMessages.ErrorMessageSameCourse();
                        return View();
                    }
                }
                if ((courseSession.Duration) < 0)
                {
                    ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
                    ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName");
                    ViewBag.ErrorMessage = ErrorMessages.ErrorMessageNegativeTime();
                    return View();
                }
                if(courseSession.DayOfWeek.ToString() != courseSession.DateTime.DayOfWeek.ToString())
                {
                    ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
                    ViewBag.InstructorID = new SelectList(db.People, "ID", "LastName");
                    ViewBag.ErrorMessage = ErrorMessages.ErrorMessageNotSameDay();
                    return View();
                }

                db.CourseSessions.Add(courseSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", courseSession.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", courseSession.InstructorID);
            return View(courseSession);
        }

        // GET: CourseSessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSession courseSession = db.CourseSessions.Find(id);
            if (courseSession == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", courseSession.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", courseSession.InstructorID);
            return View(courseSession);
        }

        // POST: CourseSessions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InstructorID,CourseID,DayOfWeek,DateTime,HourStart,HourEnd")] CourseSession courseSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", courseSession.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "LastName", courseSession.InstructorID);
            return View(courseSession);
        }

        // GET: CourseSessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSession courseSession = db.CourseSessions.Find(id);
            if (courseSession == null)
            {
                return HttpNotFound();
            }
            return View(courseSession);
        }

        // POST: CourseSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseSession courseSession = db.CourseSessions.Find(id);
            db.CourseSessions.Remove(courseSession);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
