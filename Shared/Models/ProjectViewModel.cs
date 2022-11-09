using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Shared.Models
{
    public class ProjectViewModel
    {
        public string Creator { get; set; }
        public int ProjectId { get; set; }
        public string FirstNameOfCreator { get; set; }
        public string LastNameOfCreator { get; set; }
        public int ParticipantCount { get; set; }
        public int PublicParticipantCount { get; set; }
        public List<CV> Participants { get; set; }
        public List<CV> PublicParticipants { get; set; }
        public CV Join { get; set; }
        public CV Leave { get; set; }
        public bool AllowEdit { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool isParticipating { get; set; }
    }
}
