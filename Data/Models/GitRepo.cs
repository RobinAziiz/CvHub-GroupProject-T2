using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class GitRepo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string html_url { get; set; }
    }
}
