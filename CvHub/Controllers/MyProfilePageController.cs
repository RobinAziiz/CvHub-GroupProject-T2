using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;

namespace CvHub.Controllers
{
    public class MyProfilePageController : Controller
    {
        // GET: MyProfilePage
        private CvHelper cvHelper
        {
            get
            { 
                return new CvHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        public ActionResult Index()
        {
            var creator = User.Identity.GetUserId();
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            if (CV == null) return RedirectToAction("RegisterCV", "Account");
            var viewmodel = cvHelper.CreatMyCvViewModel(creator);
            viewmodel.allowEdit = true;
            return View(viewmodel);
        }

        // GET: MyProfilePage/Details/5
        public ActionResult Details(int id)
        {
            var CV = cvHelper.GetCvOnId(id);
            var viewmodel = cvHelper.CreatMyCvViewModel(CV.Creator);
            viewmodel.allowEdit = false;
            if (User.Identity.GetUserId() != null)
            {
                var user = User.Identity.GetUserId();
                var visitorCV = cvHelper.GetCvOnCreator(user);
                viewmodel.VisitorCvId = visitorCV.Id;
            }
            //IncrementVisitCount(CV);
            return View("Index",viewmodel);
        }

        public ActionResult DeactivateCV()
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var creator = User.Identity.GetUserId();
            var CV = cvHelper.GetCvOnCreator(creator);
            CV.Deactivated = true;
            context.Entry(CV).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("index", "home");
        }

        public ActionResult ActivateCV()
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var creator = User.Identity.GetUserId();
            var CV = cvHelper.GetCvOnCreator(creator);
            CV.Deactivated = false;
            context.Entry(CV).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("index", "MyProfilePage");
        }


    }
}
