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
using Microsoft.AspNet.Identity.Owin;
using Shared;
using Shared.Models;
using WebGrease.Css.Extensions;


namespace CvHub.Controllers
{
    public class SearchController : Controller
    {

        private SkillsRepository SkillsRepository
        {
            get { return new SkillsRepository(HttpContext.GetOwinContext().Get<ApplicationDbContext>()); }
        }


        [HttpPost]
        public ActionResult Index(string searchString)
        {
            using (var context = new ApplicationDbContext())
            {
                HomeViewModel viewModel = new HomeViewModel();

                var cv = from c in context.CVs.Where(x=> x.Deactivated == false)
                    select c;
                var project = from p in context.Projects
                    select p;


                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    string[] words = searchString.Split(' ');
                    string skillNameInSearchString = null;
                    foreach (var word in words) //Kolla ifall någon av orden i sökningen är en skill
                    {
                        var skills = SkillsRepository.GetSkillByName(word);
                        if (skills != null && skills.SkillName.ToLower().Contains(word))
                        {
                            skillNameInSearchString = word; //Med denna lösning kan man dock bara söka på en skill, söker man på fler hittas enbart den sista.
                        }

                    }

                    //Ifall vi har en skill i sökningen, solla ut den från namnet i sökstringen.
                    string nameString = null;
                    if (skillNameInSearchString != null && searchString.Length != skillNameInSearchString.Length)
                    {
                        int i = searchString.IndexOf(skillNameInSearchString) -1;
                        int remove = skillNameInSearchString.Length +1;
                        nameString = searchString.Remove(i, remove);
                    }

                    //Sökning för CVs som är private & public
                    var newPrivateCvs = cv.Where(s =>
                            s.FirstName.Contains(searchString) || s.LastName.Contains(searchString) ||
                            (s.FirstName + " " + s.LastName).Contains(searchString) ||
                            s.Skills.Any(x=> x.SkillName.ToLower().Contains(skillNameInSearchString)) && searchString.Length == skillNameInSearchString.Length ||
                            s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && s.FirstName.Contains(nameString) ||
                            s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && s.LastName.Contains(nameString) ||
                            (s.FirstName + " " + s.LastName).Contains(nameString) && s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString))
                    );

                    viewModel.CVList = newPrivateCvs.ToList();

                    //Sökning för projekt
                    var newProjects = project.Where(s => s.Title.Contains(searchString) || s.Creator.Contains(searchString));
                    viewModel.ProjectList = newProjects.ToList();


                    //Sökning för CVn som är public
                    var newPubliCVs = cv.Where(s =>
                        s.FirstName.Contains(searchString) && !s.Private || s.LastName.Contains(searchString) && !s.Private ||
                        (s.FirstName + " " + s.LastName).Contains(searchString) && !s.Private ||
                        s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && searchString.Length == skillNameInSearchString.Length && !s.Private ||
                        s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && s.FirstName.Contains(nameString) && !s.Private ||
                        s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && s.LastName.Contains(nameString) && !s.Private ||
                        (s.FirstName + " " + s.LastName).Contains(nameString) && s.Skills.Any(x => x.SkillName.ToLower().Contains(skillNameInSearchString)) && !s.Private



                        );
                    viewModel.CVListPublic = newPubliCVs.ToList();

                    return View(viewModel);
                }


                return RedirectToAction("Index", "Home");
            }
        }

    }
}
