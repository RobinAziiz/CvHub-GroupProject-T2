using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Data.Repositories;
using Services;
using Shared;

namespace Helpers
{
    public class PreviousExperienceHelper
    {
        private readonly ApplicationDbContext _context;
        private PreviousExperienceRepository previousExperienceRepository
        {
            get { return new PreviousExperienceRepository(_context ?? new ApplicationDbContext()); }
        }
        private CvHelper cvHelper
        {
            get
            {
                return new CvHelper(_context ?? new ApplicationDbContext());
            }
        }
        public PreviousExperienceHelper() { }

        public PreviousExperienceHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void createPreviousExperience(DateTime startYear, DateTime endYear, string workplaceName, string description, string workplaceTitle)
        {
            var newPreviousExperience = new PreviousExperience()
            {
                StartYear = startYear,
                EndYear = endYear,
                WorkplaceName = workplaceName,
                Description = description,
                WorkplaceTitle = workplaceTitle
            };

            newPreviousExperience.Creator = cvHelper.GetCvOnCreator(cvHelper.GetUserId()).Id;
            previousExperienceRepository.Create(newPreviousExperience);
        }
    }
}
