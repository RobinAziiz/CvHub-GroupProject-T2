using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Profession
    {
        [Key]
        public int Id { get; set; }
        public string ProfessionName { get; set; }
        public virtual ICollection<CV> Users { get; set; }
    }
}
