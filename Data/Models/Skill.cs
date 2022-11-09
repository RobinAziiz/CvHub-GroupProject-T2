using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<CV> Users { get; set; }
        
        public string SkillName { get; set; }
    }

    public class SkillToSerialize
    {
        public string SkillName { get; set; }
    }
}
