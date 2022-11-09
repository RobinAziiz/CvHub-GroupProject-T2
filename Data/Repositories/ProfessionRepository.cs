using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public class ProfessionRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Profession GetProfessionById(int id)
        {
            return _context.Professions
                .FirstOrDefault(x => x.Id == id);
        }

        public Profession GetProfessionByName(string name)
        {
            return _context.Professions.FirstOrDefault(x => x.ProfessionName == name);
        }
        public List<Profession> GetAllProfessions()
        {
            return _context.Professions
                .ToList();
        }

        public string GetProfessionNameById(int id)
        {
            return _context.Professions.FirstOrDefault(x => x.Id == id).ProfessionName;
        }

        public void Create(Profession profession)
        {
            _context.Professions.Add(profession);
            _context.SaveChanges();
        }

    }

}
