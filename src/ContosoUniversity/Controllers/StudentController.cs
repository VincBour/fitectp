using ContosoUniversity.Controle;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        #region Methods preexisting
        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }


        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        //getting the image
                        string fileName = System.IO.Path.GetExtension(upload.FileName);
                        //call of the verification class CheckImage
                        CheckImage check = new CheckImage();

                        //call of the verification Extension method
                        bool extensionIsTrue = check.checkExtension(fileName);

                        if (extensionIsTrue == false)
                        {
                            ViewBag.ErrorType = "Image extention authorized is png or jpeg";
                            return View();
                        }


                        //call of the verfication Size method
                        bool sizeIsCorrect = check.checkSize(upload.ContentLength);

                        if (sizeIsCorrect == false)
                        {
                            ViewBag.ErrorSize = "The size of the image is limited to 100kb";
                            return View();
                        }

                        var avatar = new FileImage
                        {
                            FileName = Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        student.Files = new List<FileImage> { avatar };
                    }
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }


        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        string typeFile = Path.GetExtension(upload.FileName);
                        CheckImage check = new CheckImage();
                        //getting the image
                        string fileName = System.IO.Path.GetExtension(upload.FileName);

                        //call of the verification Extension method
                        bool extensionIsTrue = check.checkExtension(fileName);

                        if (extensionIsTrue == false)
                        {
                            ViewBag.ErrorType = "Image extention authorized is png or jpeg";
                            return View();
                        }

                        //call of the verfication Size method
                        bool sizeIsCorrect = check.checkSize(upload.ContentLength);

                        if (sizeIsCorrect == false)
                        {
                            ViewBag.ErrorSize = "The size of the image is limited to 100kb";
                            return View();
                        }

                        if (studentToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(studentToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new FileImage
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        studentToUpdate.Files = new List<FileImage> { avatar };

                    }
                
                    db.Entry(studentToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }
            return View(studentToUpdate);
    }

    // GET: Student/Delete/5
    public ActionResult Delete(int? id, bool? saveChangesError = false)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        if (saveChangesError.GetValueOrDefault())
        {
            ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
        }
        Student student = db.Students.Find(id);
        if (student == null)
        {
            return HttpNotFound();
        }
        return View(student);
    }

    // POST: Student/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id)
    {
        try
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }
        catch (RetryLimitExceededException/* dex */)
        {
            //Log the error (uncomment dex variable name and add a line here to write a log.
            return RedirectToAction("Delete", new { id = id, saveChangesError = true });
        }
        return RedirectToAction("Index");
    }

    public ActionResult StudentEnrollment(int id)
    {
        StudentEnrollment model = new StudentEnrollment();
        model.Student = db.Students.FirstOrDefault(s => s.ID == id);

        List<Course> CourseEnrolled = new List<Course>();
        foreach (Enrollment item in model.Student.Enrollments)
        {
            CourseEnrolled.Add(db.Courses.FirstOrDefault(c => c.CourseID == item.CourseID));
        }
        List<int> EnrolledCoursesId = CourseEnrolled.Select(q => q.CourseID).ToList();

        ViewBag.CourseID = new SelectList(db.Courses.Where(q => !EnrolledCoursesId.Contains(q.CourseID)), "CourseID", "Title");
        ViewBag.StudentID = db.Students.Find(id).ID;
        return View();
    }

    // POST: Enrollments/Create
    // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
    // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult StudentEnrollment([Bind(Include = "EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
    {
        if (ModelState.IsValid)
        {
            db.Enrollments.Add(enrollment);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = enrollment.StudentID });
        }

        ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollment.CourseID);
        ViewBag.StudentID = new SelectList(db.People, "ID", "LastName", enrollment.StudentID);
        return View(enrollment);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
    #endregion

}
}
