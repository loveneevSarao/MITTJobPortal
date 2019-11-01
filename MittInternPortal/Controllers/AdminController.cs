using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MittInternPortal.Models;

namespace MittInternPortal.Controllers
{
    public class AdminController : Controller
    {
        Models.ApplicationDbContext db = new ApplicationDbContext();
        RoleManager<IdentityRole> rolesManager;
        UserManager<Models.ApplicationUser> usersManager;

        public AdminController()
        {
            rolesManager = new RoleManager<IdentityRole>
          (new RoleStore<IdentityRole>(db));
            usersManager = new UserManager<ApplicationUser>
          (new UserStore<ApplicationUser>(db));
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Assign Roles to Users from Admin
        public ActionResult AssignRoles()
        {
            ViewBag.userId = new SelectList(db.Users.ToList(), "Id", "UserName");
            ViewBag.role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AssignRoles(string userId, string role)
        {
            usersManager.AddToRole(userId.ToString(), role);
            return RedirectToAction("Index");
        }


        //unassign user from role
        public ActionResult RemoveUsers()
        {
            ViewBag.userId = new SelectList(db.Users.ToList(), "Id", "UserName");
            ViewBag.role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult RemoveUsers(string userId, string role)
        {
            UserManagment manager = new UserManagment(db);
            var result = manager.RemoveUserFromRole(userId, role);
            return View(result);
        }
        public ActionResult InstructorDashBoard()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("InstructorDashBoard");
            }
            return View();
        }
    }
}