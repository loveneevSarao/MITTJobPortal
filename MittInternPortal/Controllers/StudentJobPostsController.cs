using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MittInternPortal.Models;

namespace MittInternPortal.Controllers
{
    public class StudentJobPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentJobPosts
        public ActionResult Index()
        {
            var studentJobPosts = db.StudentJobPosts.Include(s => s.JobPost).Include(s => s.Student);
            return View(studentJobPosts.ToList());
        }

        // GET: StudentJobPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentJobPost studentJobPost = db.StudentJobPosts.Find(id);
            if (studentJobPost == null)
            {
                return HttpNotFound();
            }
            return View(studentJobPost);
        }

        // GET: StudentJobPosts/Create
        public ActionResult Create()
        {
            ViewBag.JobPostId = new SelectList(db.JobPosts, "Id", "CompanyName");
            ViewBag.StudentId = new SelectList(db.Student, "Id", "ApplicationUserId");
            return View();
        }

        // POST: StudentJobPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobPostId,StudentId")] StudentJobPost studentJobPost)
        {
            if (ModelState.IsValid)
            {
                db.StudentJobPosts.Add(studentJobPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobPostId = new SelectList(db.JobPosts, "Id", "CompanyName", studentJobPost.JobPostId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "ApplicationUserId", studentJobPost.StudentId);
            return View(studentJobPost);
        }

        // GET: StudentJobPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentJobPost studentJobPost = db.StudentJobPosts.Find(id);
            if (studentJobPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobPostId = new SelectList(db.JobPosts, "Id", "CompanyName", studentJobPost.JobPostId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "ApplicationUserId", studentJobPost.StudentId);
            return View(studentJobPost);
        }

        // POST: StudentJobPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobPostId,StudentId")] StudentJobPost studentJobPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentJobPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobPostId = new SelectList(db.JobPosts, "Id", "CompanyName", studentJobPost.JobPostId);
            ViewBag.StudentId = new SelectList(db.Student, "Id", "ApplicationUserId", studentJobPost.StudentId);
            return View(studentJobPost);
        }

        // GET: StudentJobPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentJobPost studentJobPost = db.StudentJobPosts.Find(id);
            if (studentJobPost == null)
            {
                return HttpNotFound();
            }
            return View(studentJobPost);
        }

        // POST: StudentJobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentJobPost studentJobPost = db.StudentJobPosts.Find(id);
            db.StudentJobPosts.Remove(studentJobPost);
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
