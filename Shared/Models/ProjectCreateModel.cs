using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data.Models;

namespace Shared.Models
{
    public class ProjectCreateModel
    {
        [Display(Name = "Title")] 
        [Required]
        public string Title { get; set; }
        [Display(Name = "Description")] 
        [Required]
        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public int Id { get; set; }

    }
    public class ProjectListModels
    {
        public List<Project> ProjectList { get; set; }

    }
}

