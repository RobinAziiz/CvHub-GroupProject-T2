using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "When did the education start?")]
        public DateTime  StartYear { get; set; }
        [Display(Name = "When did/will the education be concluded?")]
        public DateTime EndYear { get; set; }
        [Required]
        [Display(Name = "What was the name of the school?")]
        public string SchoolName { get; set; }
        [Required]
        [Display(Name = "What did you study?")]
        public string EducationName { get; set; }
        [Display(Name = "What was the level of the education?")]
        public string Description { get; set; }
        [ForeignKey("Cv")]
        public int Creator { get; set; }
        public CV Cv { get; set; }

    }

    public class EducationsToSerialize
    {
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public string SchoolName { get; set; }
        public string EducationName { get; set; }
        public string Description { get; set; }
    }

}
