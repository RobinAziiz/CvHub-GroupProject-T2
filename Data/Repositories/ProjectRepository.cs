using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public class ProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project GetMostRecentlyAddedProject()
        {
            var highestID = 0;
            foreach (var project in GetAllProjects())
            {
                if (project.Id > highestID)
                {
                    highestID = project.Id;
                }
            }

            return GetProject(highestID);
        }

        public Project GetProject(int id)
        {
            return _context.Projects
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }
        public void Create(Project project)
        {
            _context.Projects.Add(project);
            SaveChanges(project);
        }

        public void SaveChanges(Project project)
        {
            _context.SaveChanges();
        }

        public void CvJoinProject(CV cv, Project project)
        {
            project.Users.Add(cv);
            cv.Projects.Add(project);
            SaveChanges(project);
        }

        public void CvLeaveProject(CV cv, Project project)
        {
            project.Users.Remove(cv);
            cv.Projects.Remove(project);
            SaveChanges(project);
        }

        public void Delete(int id)
        {
            var projectDB = _context.Projects.FirstOrDefault(x => x.Id == id);
            if (projectDB != null)
            {
                _context.Projects.Remove(projectDB);
                _context.SaveChanges();
            }

        }
    }
}
