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


namespace CvHub.Controllers
{
    public class PreviousExperienceController : Controller
    {
        private PreviousExperienceHelper PreviousExperienceHelper
        {
            get
            { // detta funderar för att vi i Startup.Configuration har sagt att vi ska skapa upp en BookContext per request, det är då en instans som delas i hela requestet
                return new PreviousExperienceHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        private PreviousExperienceRepository PreviousExperienceRepository
        {
            get
            { // detta funderar för att vi i Startup.Configuration har sagt att vi ska skapa upp en BookContext per request, det är då en instans som delas i hela requestet
                return new PreviousExperienceRepository(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        // GET: PreviousExperience
        public ActionResult Index()
        {
            return View();
        }

        // GET: PreviousExperience/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreviousExperience/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: PreviousExperience/Create
        [HttpPost]
        public ActionResult Create(PreviousExperienceViewModels model)
        {
            try
            {
                PreviousExperienceHelper.createPreviousExperience(model.StartYear, model.EndYear, model.WorkplaceName, model.Description, model.WorkplaceTitle);

            }
            catch

            {
                return View();
            }


            return RedirectToAction("Index", "MyProfilePage");
        }

        // GET: PreviousExperience/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreviousExperience/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            // Hindrar eventuell krash
            if (PreviousExperienceRepository.GetPreviousExperienceOnId(id) == null) return RedirectToAction("index", "MyProfilePage");
            PreviousExperienceRepository.Delete(id);
            return RedirectToAction("index", "MyProfilePage");
        }
    }
}
