using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CvHub.Models
{
    public class EducationViewModels
    {
        [Display(Name = "Year started:")]
        public string StartYear { get; set; }
        [Display(Name = "Year ended:")]
        public string EndYear { get; set; }
        [Display(Name = "Name of School:")]
        public string SchoolName { get; set; }
        [Display(Name = "Name of studies")]
        public string EducationName { get; set; }
        [Display(Name = "Brief description of what you did")]
        public string Description { get; set; }

    }
}