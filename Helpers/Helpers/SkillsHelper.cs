using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using Shared.Models;

namespace Helpers
{
    public class SkillsHelper
    {
        private readonly ApplicationDbContext _context;

        private SkillsRepository skillsRepository
        {
            get { return new SkillsRepository(_context ?? new ApplicationDbContext()); }
        }

        private CvHelper cvHelper
        {
            get { return new CvHelper(_context ?? new ApplicationDbContext()); }
        }

        private CVRepository CVRepository
        {
            get { return new CVRepository(_context ?? new ApplicationDbContext()); }
        }

        public SkillsHelper()
        {
        }

        public SkillsHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateSkill(string skillName)
        {
            var cv = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            var skillID = 0;
            if (skillsRepository.GetSkillByName(skillName) != null)
            {
                skillsRepository.CvJoinSkill(cv, skillsRepository.GetSkillByName(skillName));
            }
            else
            {
                var newSkill = new Skill()
                {
                    SkillName = skillName,
                    Users = new List<CV>()
                };
                skillsRepository.CvJoinSkill(cv, newSkill);
            }
        }
            
        public void JoinSkill(int skillID)
        {
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            var skill = skillsRepository.GetSkillById(skillID);
            skillsRepository.CvJoinSkill(CV, skill);
        }

        public void LeaveSkill(SkillsModifyViewModel model)
        {
            var skillID = skillsRepository.GetSkillByName(model.SkillName).Id;
            var CV = cvHelper.GetCvOnCreator(cvHelper.GetUserId());
            var skill = skillsRepository.GetSkillById(skillID);
            skillsRepository.CvLeaveSkill(CV,skill);
        }
        public SkillsModifyViewModel CreateSkillsModifyViewModel(string creator)
        {
            SkillsModifyViewModel viewmodel = new SkillsModifyViewModel();
            CV cv = cvHelper.GetCvOnCreator(creator);
            viewmodel.myCurrentSkills = cv.Skills.ToList();
            viewmodel.avalibleSkills =skillsRepository.GetAllAvalibleSkills(cv);
            viewmodel.avalibleSkillNames = skillsRepository.GetSkillNamesOnList(viewmodel.avalibleSkills);
            viewmodel.currentSkillNames = skillsRepository.GetSkillNamesOnList(skillsRepository.GetSkillOnCV(cv));
            return viewmodel;
        }
       
    }
}
