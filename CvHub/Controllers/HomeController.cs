using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CvHub.Models;
using Data;
using Data.Repositories;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shared;
using Shared.Models;

namespace CvHub.Controllers
{
    public class HomeController : Controller
    {
        private CvHelper cvHelper
        {
            get
            { 
                return new CvHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        private MessageHelper messageHelper
        {
            get
            {
                return new MessageHelper(Request.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            try
            {
                viewModel = cvHelper.CreateHomeViewModel();
                var creator = User.Identity.GetUserId();
                if (cvHelper.GetCvOnCreator(creator) == null && creator != null)
                {
                    return RedirectToAction("RegisterCV", "account");
                }

                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var creator = User.Identity.GetUserId();
            if (cvHelper.GetCvOnCreator(creator) == null && creator != null) return RedirectToAction("RegisterCV", "account");
            MessagesViewModel viewModel = messageHelper.CreateMessagesViewModel(creator);
            return View(viewModel);
        }

    }
}