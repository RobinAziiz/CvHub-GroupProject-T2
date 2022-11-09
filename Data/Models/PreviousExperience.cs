using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PreviousExperience
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "When did your employment start?")]
        public DateTime StartYear { get; set; }
        [Display(Name = "When did your employment end?")]
        public DateTime EndYear { get; set; }
        [Required]
        [Display(Name = "What was the name of the company?")]
        public string WorkplaceName { get; set; }
        [Display(Name = "What was your title?")]
        [Required]
        public string WorkplaceTitle { get; set; }
        [Display(Name = "What was your workdescription?")]
        public string Description { get; set; }
        [ForeignKey("Cv" )]
        public int Creator { get; set; }
        public CV Cv { get; set; }


    }

    public class PreviousExperienceToSerialize
    {
        public string WorkplaceName { get; set; }
        public string WorkplaceTitle { get; set; }
        public string  Description { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }

    }
}
