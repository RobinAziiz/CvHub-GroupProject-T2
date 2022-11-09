using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNet.Identity;
using Shared.Models;

namespace Helpers
{
    public class CvHelper
    {
        private readonly ApplicationDbContext _context;

        private CVRepository CVRepository
        {
            get { return new CVRepository(_context ?? new ApplicationDbContext()); }
        }
        private ProfessionRepository ProfessionRepository
        {
            get { return new ProfessionRepository(_context ?? new ApplicationDbContext()); }
        }
        private ProjectRepository ProjectRepository
        {
            get { return new ProjectRepository(_context ?? new ApplicationDbContext()); }
        }
        private SkillsRepository SkillsRepository
        {
            get { return new SkillsRepository(_context ?? new ApplicationDbContext()); }
        }

        public CvHelper()
        {
            _context = new ApplicationDbContext();
        }
        public CvHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext GetApplicationDbContext()
        {
            return _context;
        }
        public string GetUserId()
        {
           return HttpContext.Current.User.Identity.GetUserId();
        }

        // Här börjar CV-Helpers
        public CV GetCvOnCreator(string creator)
        {
            return CVRepository.GetCVCreator(creator);
        }
        public CV GetCvOnId(int id)
        {
            return CVRepository.GetCvOnId(id);
        }
        public MyCVViewModel CreatMyCvViewModel(string creator)
        {
            MyCVViewModel viewmodel = new MyCVViewModel();
            try
            {
                CV cv = GetCvOnCreator(creator);
                if (cv == null) throw new Exception();
                viewmodel.Id = cv.Id;
                viewmodel.FirstName = cv.FirstName;
                viewmodel.LastName = cv.LastName;
                viewmodel.Id = cv.Id;
                viewmodel.Adress = cv.Adress;
                viewmodel.Phonenumber = cv.PhoneNumber;
                viewmodel.Email = cv.Email;
                viewmodel.Creator = cv.Creator;
                if (cv.Projects.Count() != 0)
                {
                    viewmodel.isProjectsEmpty = false;
                    viewmodel.Projects = cv.Projects.ToList();
                }
                else viewmodel.isProjectsEmpty = true;

                var nonting = ProfessionRepository.GetProfessionById(cv.Profession);
                viewmodel.ProfessionName = nonting.ProfessionName;
                viewmodel.Bio = cv.Bio;
                if (cv.Educations.Count() != 0)
                {
                    viewmodel.Educations = cv.Educations.ToList();
                    viewmodel.isEducationsEmpty = false;
                }
                else viewmodel.isEducationsEmpty = true;

                if (cv.Skills.Count() != 0)
                {
                    viewmodel.isSkillsEmpty = false;
                    viewmodel.Skills = cv.Skills.ToList();
                }
                else viewmodel.isSkillsEmpty = true;

                viewmodel.BirthDate = cv.BirthDate;
                if (cv.PreviousExperience.Count() != 0)
                {
                    viewmodel.isPreviousExperiencesEmpty = false;
                    viewmodel.PreviousExperience = cv.PreviousExperience.ToList();
                }
                else viewmodel.isPreviousExperiencesEmpty = true;

                viewmodel.ImagePath = cv.ImagePath;
                viewmodel.Deactivated = cv.Deactivated;
                if (cv.Repos.Count() != 0)
                {
                    viewmodel.isReposEmpty = false;
                    viewmodel.Repos = cv.Repos.ToList();
                }
                else viewmodel.isReposEmpty = true;

                if (FindSimilarCv(cv) == null || FindSimilarCv(cv).Id == 0)
                {
                    viewmodel.SimilarCv = null;
                }
                else viewmodel.SimilarCv = FindSimilarCv(cv);

                return viewmodel;
            }
            catch (Exception ex)
            {
                return viewmodel;
            }
        }
        public CV FindSimilarCv(CV cv)
        {
            // Hindrar eventuell krash.
            if (CVRepository.GetAllCvs().Count == 0) return cv;
            // Först kollar vi om det finns några användare med samma yrke som profilen vi försöker besöka. Vi exkluderar profilens egna cv från listan.
            var cvWithSameProfession = CVRepository.RemovePrivateAndDeactivatedCvs(CVRepository.ExcludeCvInList(CVRepository.GetCvsOnProfession(cv.Profession), cv));
            if (cvWithSameProfession.Count() != 0)
            {
                // Här har vi en lista på användare med samma yrke
                // Vi går då igenom de och kollar vem utav de som har flest matchande Skills/Egenskaper.
                var numberOfMatches = 0;
                var highestMatchingSkills = 0;
                // Vi initialiserar variabeln mest matchande cv som första CVet i listan samma yrke. 
                // Detta gör att om ingen av personerna i samma yrka har någon skill gemensam med profilen vi besöker
                // Så returneras första cvet i listan.
                var cvWithMostMatchingSkills = cvWithSameProfession[0];
                foreach (var CV in cvWithSameProfession)
                {
                    var cvSkills = CV.Skills.ToList();
                    var ogCvSkills = cv.Skills.ToList();
                    foreach (var skill in cvSkills)
                    {
                        if (ogCvSkills.Contains(skill))
                        {
                            numberOfMatches++;
                        }
                    }

                    if (numberOfMatches > highestMatchingSkills)
                    {
                        highestMatchingSkills = numberOfMatches; 
                        cvWithMostMatchingSkills = CV;
                        numberOfMatches = 0;
                    }
                }
                return cvWithMostMatchingSkills;
            }
            // Om det inte finns något annat CV med samma yrke så letar vi enbart efter vem som har flest skills/egenskaper gemensamt.
            else
            {
                var numberOfMatches = 0;
                var highestMatchingSkills = 0;
                // Här initialiseras Cvet med flest matchade skills som ett tomt CV-objekt så vi vet att den inte returnerar null
                var cvWithMostMatchingSkills = new CV();
                var cvWithoutSameProfession = CVRepository.RemovePrivateAndDeactivatedCvs(CVRepository.ExcludeCvInList(CVRepository.ExcludeDeactivatedAccounts(CVRepository.GetAllCvs()), cv));
                foreach (var CV in cvWithoutSameProfession)
                {
                    var cvSkills = SkillsRepository.GetSkillNamesOnList(CV.Skills.ToList());
                    var ogCvSkills = SkillsRepository.GetSkillNamesOnList(cv.Skills.ToList());
                    foreach (var skill in cvSkills)
                    {
                        if (ogCvSkills.Contains(skill))
                        {
                            numberOfMatches++;
                        }
                    }
                    if (numberOfMatches >= highestMatchingSkills)
                    {
                        highestMatchingSkills = numberOfMatches;
                        cvWithMostMatchingSkills = CV;
                        numberOfMatches = 0;
                    }
                }
                return cvWithMostMatchingSkills;
            }
        }
        public List<CV> GetParticipantsWithPublicCV(Project project)
        {
            var allParticipants = project.Users.ToList();
            var returnList = new List<CV>();
            foreach (var participant in allParticipants)
            {
                if (!participant.Private)
                {
                    returnList.Add(participant);
                }
            }

            return returnList;
        }

        // Här slutar Cv-Helpers
        //-----------------------------
        // Helper för att fylla Gender-ComboBox
        public List<string> GetGenders()
        {
            var returnlist = new List<string>();
            returnlist.Add("Male");
            returnlist.Add("Female");
            returnlist.Add("Unspecified");
            return returnlist;
        }

        // Helper för att skapa HomePage
        public HomeViewModel CreateHomeViewModel()
        {
            var viewModel = new HomeViewModel();
            viewModel.CVList = CVRepository.GetRandomCVs(3);
            viewModel.CVListPublic = CVRepository.GetRandomPublicCVs(3);
            viewModel.LatestProject = ProjectRepository.GetMostRecentlyAddedProject();
            return viewModel;
        }
    }
}