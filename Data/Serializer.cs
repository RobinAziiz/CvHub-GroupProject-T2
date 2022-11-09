using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Data.Models;


namespace Data
{
    public class Serializer
    {
     
        
        public string mySerializer(string user, int userid)
        {
            using (var context = new ApplicationDbContext())
            {
                var cv = context.CVs.FirstOrDefault(x => x.Id == userid);

                //Eftersom det inte går att serialisera ICollections så har vi skapat nya modeller
                //för att serialisera, och eftersom en användare kan ha flera projekt, utbildningar osv
                //så skapar vi arrayer av dessa modeller. 
                var userProjects = context.Projects.Where(x => x.Creator == user);
                ProjectToSerialize[] projectArray = new ProjectToSerialize[userProjects.Count()];
                int i = 0;
                if (userProjects != null)
                {
                    foreach (var project in userProjects)
                    {
                        projectArray[i++] = new ProjectToSerialize()
                        {
                            Title = project.Title,
                            Description = project.Description,
                        };
                    }
                }


                i = 0;
                var userExperiences = context.PreviousExperiences.Where(x => x.Creator == userid);
                PreviousExperienceToSerialize[] experienceArray = new PreviousExperienceToSerialize[userExperiences.Count()];
                if (userExperiences != null)
                {
                    foreach (var experience in userExperiences)
                    {
                        experienceArray[i++] = new PreviousExperienceToSerialize()
                        {
                            WorkplaceName = experience.WorkplaceName,
                            WorkplaceTitle = experience.WorkplaceTitle,
                            Description = experience.Description,
                            StartYear = experience.StartYear,
                            EndYear = experience.EndYear
                        };
                    }
                }

                i = 0;
                var userEducation = context.Educations.Where(x => x.Creator == userid);
                EducationsToSerialize[] educationsArray = new EducationsToSerialize[userEducation.Count()];
                if (userEducation != null)
                {
                    foreach (var education in userEducation)
                    {
                        educationsArray[i++] = new EducationsToSerialize()
                        {
                            SchoolName = education.SchoolName,
                            EducationName = education.EducationName,
                            Description = education.Description,
                            StartYear = education.StartYear,
                            EndYear = education.EndYear
                        };

                    }
                }

                i = 0;
                var userSkills = context.Skills.Where(x => x.Users.Any(y => y.Id == userid));
                SkillToSerialize[] skillsArray = new SkillToSerialize[userSkills.Count()];
                if (userSkills != null)
                {
                    foreach (var skill in userSkills)
                    {
                        skillsArray[i++] = new SkillToSerialize()
                        {
                            SkillName = skill.SkillName
                        };

                    }
                }

                var profession = context.Professions.FirstOrDefault(x => x.Id == cv.Profession);

                //Skapar CVt som ska serialiseras med hjälp av alla arrayer vi skapat och vad vi kan hämta direkt från CVt
                CVToSerialize testcv = new CVToSerialize()
                {
                    FirstName = cv.FirstName,
                    LastName = cv.LastName,
                    BirthDate = cv.BirthDate,
                    Gender = cv.Gender,
                    Bio = cv.Bio,
                    Profession = profession.ProfessionName,
                    Adress = cv.Adress,
                    PhoneNumber = cv.PhoneNumber,
                    Visits = cv.Visits,
                    Projects = projectArray,
                    PreviousExperience = experienceArray,
                    Skills = skillsArray,
                    Educations = educationsArray,
                    Private = cv.Private

                };
                XmlSerializer serializer = new XmlSerializer(typeof(CVToSerialize));

                
                //Ge filen ett unikt namn (baserat på dag och tid + namn) och spara ner den i uploaded.
                var filename = DateTime.Now.ToString("mmddyyyyhhmmss") + "_" + cv.FirstName + cv.LastName +".xml";
                var filepath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                var fullPath = filepath + "/" +filename;
                using (FileStream fs = File.Create(fullPath))
                {
                    serializer.Serialize(fs, testcv);
                    fs.Close();
                }
                return fullPath;
            }
        }

    }
}
