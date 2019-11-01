using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MittInternPortal.Models;

namespace MittInternPortal.Controllers
{
    public class JobPostsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private JobManagement JobManager;
        public JobPostsController()
        {
            JobManager = new JobManagement(db);
        }

        // GET: JobPosts
        public ActionResult Index()
        {
            var jobPosts = db.JobPosts.Include(j => j.Employers).Include(j => j.Round);
            return View(jobPosts.ToList());
        }

        // GET: JobPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // GET: JobPosts/Create
        public ActionResult Create()
        {
            ViewBag.EmployerId = new SelectList(db.Employer, "Id", "CompanyName");
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Session");
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployerId,RoundId,CompanyAddress,Position,Posted,Description")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {//
                db.JobPosts.Add(jobPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployerId = new SelectList(db.Employer, "Id", "CompanyName", jobPost.EmployerId);
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Session", jobPost.RoundId);
            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployerId = new SelectList(db.Employer, "Id", "CompanyName", jobPost.EmployerId);
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Session", jobPost.RoundId);
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployerId,RoundId,CompanyAddress,Position,Posted,Description")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployerId = new SelectList(db.Employer, "Id", "CompanyName", jobPost.EmployerId);
            ViewBag.RoundId = new SelectList(db.Rounds, "Id", "Session", jobPost.RoundId);
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPost jobPost = db.JobPosts.Find(id);
            db.JobPosts.Remove(jobPost);
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
        public ActionResult JobList()
        {
            var jobPost = db.JobPosts.ToList();
            return View(jobPost);
        }
        [HttpGet]
        public ActionResult SubmitResume()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitResume(HttpPostedFileBase file /*Resume r*/)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedResume"), _FileName);
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
        public ActionResult MyJobPost()
        {
            if (User.IsInRole("Employer"))
            {
                var userId = User.Identity.GetUserId();
                var allJobsForUser = JobManager.GetUserTickets(userId).ToList();
                return View("Index", allJobsForUser);
            }
            return View();

        }
    }
}
