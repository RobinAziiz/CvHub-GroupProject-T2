using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using Services;
using Shared;
using Shared.Models;

namespace Helpers
{
    public class ProjectHelper
    {
        private readonly ApplicationDbContext _context;

        private ProjectRepository projectRepository
        {
            get { return new ProjectRepository(_context ?? new ApplicationDbContext()); }
        }

        private CvHelper cvHelper
        {
            get { return new CvHelper(_context ?? new ApplicationDbContext()); }
        }
        private CVRepository CVRepository
        {
            get { return new CVRepository(_context ?? new ApplicationDbContext()); }
        }

        public ProjectHelper()
        {
        }

        public ProjectHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public int createProject(ProjectCreateModel model, string filepath)
        {
            var creator = cvHelper.GetUserId();
            var newproject = new Project()
            {
                Title = model.Title,
                Description = model.Description,
                Creator = creator,
            };
            var filename = model.Image.FileName;
            model.Image.SaveAs(filepath + "/" + filename);
            newproject.ImagePath = filename;
            projectRepository.Create(newproject);
            var createdProjectWithId = _context.Projects
                .Where(x => x.Title == model.Title && x.Description == model.Description && x.Creator == creator)
                .FirstOrDefault();
            return createdProjectWithId.Id;

        }

        public void JoinProject(int projectID)
        {
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            var project = projectRepository.GetProject(projectID);
            projectRepository.CvJoinProject(CV, project);

        }

        public void LeaveProject(int projectID)
        {
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            var project = projectRepository.GetProject(projectID);
            projectRepository.CvLeaveProject(CV, project);
        }

        public void ApplyEdit(ProjectCreateModel model, int id, string filepath)
        {

            var project = projectRepository.GetProject(id);
            project.Description = model.Description;
            //project.ImagePath = model.ImagePath;
            project.Title = model.Title;

            if (model.Image != null)
            {
                var filename = model.Image.FileName;
                model.Image.SaveAs(filepath + "/" + filename);
                project.ImagePath = filename;
            }

            projectRepository.SaveChanges(project);
        }
        public ProjectViewModel CreateProjectViewModel(Project project)
        {

            ProjectViewModel viewModel = new ProjectViewModel();
            viewModel.Participants = CVRepository.ExcludeDeactivatedAccounts(project.Users.ToList());
            viewModel.PublicParticipants = CVRepository.ExcludeDeactivatedAccounts(cvHelper.GetParticipantsWithPublicCV(project));
            viewModel.ParticipantCount = viewModel.Participants.Count();
            viewModel.PublicParticipantCount = viewModel.PublicParticipants.Count();
            viewModel.Creator = project.Creator;
            viewModel.FirstNameOfCreator = cvHelper.GetCvOnCreator(project.Creator).FirstName;
            viewModel.LastNameOfCreator = cvHelper.GetCvOnCreator(project.Creator).LastName;
            viewModel.ImagePath = project.ImagePath;
            viewModel.Title = project.Title;
            viewModel.Description = project.Description;
            viewModel.Id = project.Id;
            viewModel.ProjectId = project.Id;
            return viewModel;
        }
        public ProjectCreateModel CreateProjectCreateModel(Project project)
        {
            ProjectCreateModel viewModel = new ProjectCreateModel();
            viewModel.Description = project.Description;
            viewModel.Title = project.Title;
            viewModel.Id = project.Id;
            return viewModel;
        }
    }
}