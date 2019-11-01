using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MittInternPortal.Models;
using System.IO;

namespace MittInternPortal.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Student.Include(s => s.ApplicationUser).Include(s => s.Instructor);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName");
            ViewBag.InstructorId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationUserId,Address,BirthDate,Gender,Contact,InstructorId")] Student student, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Image"), _FileName);
                    file.SaveAs(_path);
                    //r.Name = _FileName;
                    //db.Resume.Add(r);
                    //db.SaveChanges();

                }
                ViewBag.Message = "File Uploded Successfully!";
                //return View();
            }
            catch
            {
                ViewBag.Message = "File Upload failed!";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", student.ApplicationUserId);
            ViewBag.InstructorId = new SelectList(db.Users, "Id", "FullName", student.InstructorId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", student.ApplicationUserId);
            ViewBag.InstructorId = new SelectList(db.Users, "Id", "FullName", student.InstructorId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApplicationUserId,Address,BirthDate,Gender,Contact,InstructorId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", student.ApplicationUserId);
            ViewBag.InstructorId = new SelectList(db.Users, "Id", "FullName", student.InstructorId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(student);
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
        public ActionResult StudentDashBoard()
        {
            if (User.IsInRole("Admin"))
            {
                RedirectToAction("JobList", "JobPosts");
            }
            else
            {
                RedirectToAction("JobList", "JobPosts");
            }
            return View();
        }
        //upload image
        public ActionResult UplaodImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UplaodImage(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Image"), _FileName);
                    file.SaveAs(_path);
                    //r.Name = _FileName;
                    //db.Resume.Add(r);
                    //db.SaveChanges();

                }
                ViewBag.Message = "File Uploded Successfully!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File Upload failed!";
                return View();
            }
        }
    }
}
