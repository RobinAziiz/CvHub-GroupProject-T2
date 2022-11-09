using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Shared.Models
{
    public class MessagesViewModel
    {
        public List<Message> Messages { get; set; }
        public int RecieverId { get; set; }
    }
}
