using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Shared.Models
{
    public class HomeViewModel
    {
        public List<CV> CVList { get; set; }
        public List<CV> CVListPublic { get; set; }
        public List<Project> ProjectList { get; set; }
        public Project LatestProject { get; set; }
    }
}
