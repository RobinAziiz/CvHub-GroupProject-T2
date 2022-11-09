using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CvHub.Models;
using Data;
using Data.Models;
using Data.Repositories;
using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services;
using Shared;

namespace CvHub.Controllers
{
    public class EducationController : Controller
    {
        private EducationRepository EducationRepository
        {
            get
            { 
                return new EducationRepository(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        private EducationHelper educationHelper
        {
            get
            {
                return new EducationHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }

        // GET: Education/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Education/Create
        [HttpPost]
        public ActionResult Create(EducationViewModels model)
        {
            try
            {
                educationHelper.createEducation(model.StartYear, model.EndYear, model.SchoolName, model.Description, model.EducationName);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index", "MyProfilePage");
        }



        public ActionResult Delete(int id)
        {
            // Hindrar eventuell krash

            if (EducationRepository.GetEducationOnId(id) == null) return RedirectToAction("index", "MyProfilePage");
            EducationRepository.Delete(id);
            return RedirectToAction("index", "MyProfilePage");
        }


    }
}
