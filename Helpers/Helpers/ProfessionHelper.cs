using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Data.Repositories;

namespace Helpers
{
   public class ProfessionHelper
    {
        private readonly ApplicationDbContext _context;
        private ProfessionRepository ProfessionRepository
        {
            get { return new ProfessionRepository(_context ?? new ApplicationDbContext()); }
        }
        private CvHelper cvHelper
        {
            get
            {
                return new CvHelper(_context ?? new ApplicationDbContext());
            }
        }
        public ProfessionHelper() { }

        public ProfessionHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> GetProfessionNames()
        {
            var query = from profession in ProfessionRepository.GetAllProfessions() select profession.ProfessionName;
            return query.ToList();
        }
        public Profession GetProfessionByName(string name)
        {
            return ProfessionRepository.GetProfessionByName(name);
        }
        public Profession GetProfessionById(int id)
        {
            return ProfessionRepository.GetProfessionById(id);
        }

        public string GetProfessionNameById(int id)
        {
            return ProfessionRepository.GetProfessionNameById(id);
        }
    }
}
