using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Models;
using Data.Repositories;
using Services;
using Shared;

namespace Helpers
{
    public class EducationHelper
    {
        private readonly ApplicationDbContext _context;
        private EducationRepository EducationRepository
        {
            get { return new EducationRepository(_context ?? new ApplicationDbContext()); }
        }
        private CvHelper cvHelper
        {
            get
            {
                return new CvHelper(_context ?? new ApplicationDbContext());
            }
        }
        public EducationHelper() { }

        public EducationHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void createEducation(string startYear, string endYear, string schoolName, string description, string educationName)
        {
            var newEducation = new Education()
            {
                StartYear = DateHelper.getDateTime(startYear),
                EndYear = DateHelper.getDateTime(endYear),
                SchoolName = schoolName,
                Description = description,
                EducationName = educationName
            };

            newEducation.Creator = cvHelper.GetCvOnCreator(cvHelper.GetUserId()).Id;
            EducationRepository.Create(newEducation);
        }
    }
}
