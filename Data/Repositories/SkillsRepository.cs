using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Data.Repositories
{
    public class SkillsRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Skill GetSkillById(int id)
        {
            return _context.Skills
                .FirstOrDefault(x => x.Id == id);
        }

        public Skill GetSkillByName(string name)
        {
            return _context.Skills.FirstOrDefault(x => x.SkillName == name);
        }
        public List<Skill> GetAllSkills()
        {
            return _context.Skills
                .ToList();
        }

        public List<Skill> GetSkillOnCV(CV cv)
        {
           List<Skill> currentSkills = cv.Skills.ToList();
           return currentSkills;
        }

        public List<Skill> GetAllAvalibleSkills(CV cv)
        {

            var returnList = new List<Skill>();
            foreach (var skill in GetAllSkills())
            {
                if (!GetSkillOnCV(cv).Contains(skill))
                {
                    returnList.Add(skill);
                }
            }

            return returnList;
        }

        public List<string> GetSkillNamesOnList(List<Skill> skillList)
        {
            var returnList = new List<string>();
            foreach (var skill in skillList)
            {
                returnList.Add(skill.SkillName);
            }

            return returnList;
        }

        public void CvJoinSkill(CV cv, Skill skill)
        {
            cv.Skills.Add(skill);
            skill.Users.Add(cv);
            _context.SaveChanges();
        }
        public void CvLeaveSkill(CV cv, Skill skill)
        {
            skill.Users.Remove(cv);
            cv.Skills.Remove(skill);
            _context.SaveChanges();
        }

        public void Create(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
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