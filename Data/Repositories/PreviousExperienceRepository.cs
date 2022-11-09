using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public class PreviousExperienceRepository
    {
        private readonly ApplicationDbContext _context;
        public PreviousExperienceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public PreviousExperience GetPreviousExperienceOnId(int id)
        {
            return _context.PreviousExperiences
                .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var prevexpDB = GetPreviousExperienceOnId(id);
            if (prevexpDB != null)
            {
                _context.PreviousExperiences.Remove(prevexpDB);
                _context.SaveChanges();
            }

        }
        public void Create(PreviousExperience previousExperience)
        {
            _context.PreviousExperiences.Add(previousExperience);
            _context.SaveChanges();
        }
    }
}