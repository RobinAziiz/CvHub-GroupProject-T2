using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Shared.Models
{
     public class SkillsModifyViewModel
    {
        public List<Skill> myCurrentSkills { get; set; }
        public List<Skill> avalibleSkills { get; set; }
        [Display(Name = "Name of skill")]
        public string SkillName { get; set; }
        public List<string> avalibleSkillNames { get; set; }
        public List<string> currentSkillNames { get; set; }
    }
}
