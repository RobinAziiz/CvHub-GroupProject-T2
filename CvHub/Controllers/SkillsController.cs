using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Validation;
using System.Web.Mvc;
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
    public class SkillsController : Controller
    {
        // GET: Skills
 
        private SkillsHelper skillsHelper
        {
            get
            {
                return new SkillsHelper(HttpContext.GetOwinContext().Get<ApplicationDbContext>());
            }
        }
         public ActionResult Index(SkillsModifyViewModel model)
        {
            try
            {
                if (model.SkillName == null) return RedirectToAction("Create");
                skillsHelper.CreateSkill(model.SkillName);
                return RedirectToAction("create");
            }
            catch
            {
                return RedirectToAction("Create");
            }

        }
            public ActionResult JoinSkill(int skillID)
            {
                skillsHelper.JoinSkill(skillID);
                return RedirectToAction("Create");
            }

            public ActionResult LeaveSkill(SkillsModifyViewModel model)
            {
                try
                {
                   skillsHelper.LeaveSkill(model);
                    return RedirectToAction("Create");
                }
                catch
                {
                    return RedirectToAction("Create");
                }
            }

            // GET: Skills/Create
            public ActionResult Create()
            {
                var creator = User.Identity.GetUserId();
                var viewmodel = skillsHelper.CreateSkillsModifyViewModel(creator);
                return View(viewmodel);
            }
    }
}
