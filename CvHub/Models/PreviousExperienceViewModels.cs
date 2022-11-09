using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Data.Models;

namespace CvHub.Models
{
    public class PreviousExperienceViewModels
    {

        [Display(Name = "StartYear")]
        public DateTime StartYear{ get; set; }
        [Display(Name = "EndYear")]
        public DateTime EndYear { get; set; }

        [Display(Name="WorkplaceName")]
        public string WorkplaceName{ get; set; }
        [Display(Name="WorkplaceTitle")]
        public string WorkplaceTitle { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        public int Id { get; set; }

    }


}