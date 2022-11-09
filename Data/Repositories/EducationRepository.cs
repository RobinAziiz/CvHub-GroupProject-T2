using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
   public class EducationRepository
    {
        private readonly ApplicationDbContext _context;
        public EducationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Education GetEducationOnId(int id)
        {
            return _context.Educations
                .FirstOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            var educationDB = GetEducationOnId(id);
            if (educationDB != null)
            {
                _context.Educations.Remove(educationDB);
                _context.SaveChanges();
            }

        }

        public void Create(Education education)
        {
            _context.Educations.Add(education);
            _context.SaveChanges();
        }
    }
}
