using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; }
       
        public string MessageText { get; set; }
        public int SenderId { get; set; }
        public int RecieverID { get; set; }
        public bool Read { get; set; }
        public DateTime DateTime { get; set; }
    }
}
