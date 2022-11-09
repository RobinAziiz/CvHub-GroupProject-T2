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
using Shared;
using Shared.Models;

namespace CvHub.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectRepository projectRepository
        {
            get { return new ProjectRepository(HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }
        private CvHelper cvHelper
        {
            get
            { // detta funderar för att vi i Startup.Configuration har sagt att vi ska skapa upp en BookContext per request, det är då en instans som delas i hela requestet
                return new CvHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        private ProjectHelper projectHelper
        {
            get
            { // detta funderar för att vi i Startup.Configuration har sagt att vi ska skapa upp en BookContext per request, det är då en instans som delas i hela requestet
                return new ProjectHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
        // GET: Project
        public ActionResult Index()
        {
            var creator = User.Identity.GetUserId();
            var CV = cvHelper.GetCvOnCreator(creator);
            if (cvHelper.GetCvOnCreator(creator) == null && creator != null) return RedirectToAction("RegisterCV", "account");
            ProjectListModels viewModel = new ProjectListModels();
            using (var context = new ApplicationDbContext())
            {
                viewModel.ProjectList = context.Projects.ToList();
            }

            return View(viewModel);
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            var creator = User.Identity.GetUserId();
            var CV = cvHelper.GetCvOnCreator(creator);
            var project = projectRepository.GetProject(id);
            var viewmodel = projectHelper.CreateProjectViewModel(project);
            if (viewmodel.Creator == creator) viewmodel.AllowEdit = true;
            if (CV != null && CV.Projects.Contains(project)) viewmodel.isParticipating = true;
            return View(viewmodel);
        }

        // GET: Project/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectCreateModel model)
        {
            try
            {
                if (model.Image == null)
                {
                    return RedirectToAction("Create", new {model = model});
                }
                var filepath = Server.MapPath("~/UploadedImages");
                var projectInt = projectHelper.createProject(model, filepath);
                return RedirectToAction("joinProject", new { projectID = projectInt });
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(ProjectCreateModel model, int id)
        {
            var project = projectRepository.GetProject(id);
            model = projectHelper.CreateProjectCreateModel(project);
            return View(model);

        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult ApplyEdit(ProjectCreateModel model, int id)
        {
            try
            {
                if (model.Image != null)
                { 
                    var filepath = Server.MapPath("~/UploadedImages");
                    projectHelper.ApplyEdit(model, id, filepath);
                }
                return RedirectToAction("Details", new{ id = model.Id });
            }
            catch
            {
                return RedirectToAction("Details", new { id = model.Id });
            }
        }
        public ActionResult joinProject(int projectID)
        {
            projectHelper.JoinProject(projectID);
            return RedirectToAction("Details", new{ id = projectID});
        }
        public ActionResult LeaveProject(int projectID)
        {
            projectHelper.LeaveProject(projectID);
            return RedirectToAction("Details", new { id = projectID });
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
