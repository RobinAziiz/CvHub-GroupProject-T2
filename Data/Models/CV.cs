using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class CV
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public int Visits { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        
        public string Gender { get; set; }
        [ForeignKey("UserProfession")]
        
        public int Profession { get; set; }
        public Profession UserProfession { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<PreviousExperience> PreviousExperience { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<GitRepo> Repos { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        
        public DateTime BirthDate { get; set; }
        public string Creator { get; set; }
        public bool Private { get; set; }
        public string ImagePath { get; set; }
        public bool Deactivated { get; set; }
    }
}
