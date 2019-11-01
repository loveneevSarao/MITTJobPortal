using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MittInternPortal.Models;

namespace MittInternPortal.Controllers
{
    public class ApplyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Apply
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Apply()
        {
         
            return View();
        }

    }
}