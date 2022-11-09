using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<CV> Users { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        public string Creator { get; set; }

        public string ImagePath { get; set; }
    }

    public class ProjectToSerialize
    {
        public string Title { get; set; }
        public string Description { get; set; }


    }
}