using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data.Models;

namespace Shared.Models
{
    public class MyCVViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Visits { get; set; }
        public string Gender { get; set; }
        public string Phonenumber { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string ProfessionName { get; set; }
        public string Bio { get; set; }
        public List<Project> Projects { get; set; }
        public List<Education> Educations { get; set; }
        public List<PreviousExperience> PreviousExperience { get; set; }
        public List<Skill> Skills { get; set; }
        public List<GitRepo> Repos { get; set; }
        public DateTime BirthDate { get; set; }
        public string Creator { get; set; }
        public int VisitorCvId { get; set; }
        public bool Private { get; set; }
        public string ImagePath { get; set; }
        public bool allowEdit { get; set; }
        public bool isEducationsEmpty { get; set; }
        public bool isSkillsEmpty { get; set; }
        public bool isProjectsEmpty { get; set; }
        public bool isPreviousExperiencesEmpty { get; set; } 
        public bool isReposEmpty { get; set; }
        public bool Deactivated { get; set; }
        public CV SimilarCv { get; set; }
    }


    
}
