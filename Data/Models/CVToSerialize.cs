using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.Models
{
    public class CVToSerialize
    {
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "Profile-Visits")]
        public int Visits { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string Bio { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        [XmlElement(ElementName = "Projects")]
        public ProjectToSerialize[] Projects { get; set; }
        [XmlElement(ElementName = "Educations")]
        public EducationsToSerialize[] Educations { get; set; }
        [XmlElement(ElementName = "Previous-Experiences")]
        public PreviousExperienceToSerialize[] PreviousExperience { get; set; }
        [XmlElement(ElementName = "Skills")]
        public SkillToSerialize[] Skills { get; set; }

        [XmlElement(ElementName = "Birthdate")]
        public DateTime BirthDate { get; set; }
        public string Creator { get; set; }
        [XmlElement(ElementName = "IsProfilePrivate")]
        public bool Private { get; set; }
        [XmlElement(ElementName = "IsProfileDeactivated")]
        public bool Deactivated { get; set; }

    }
}
