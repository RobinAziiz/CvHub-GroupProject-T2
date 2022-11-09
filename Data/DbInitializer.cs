using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data.Models;

namespace Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            Profession Doctor = new Profession {ProfessionName = "Doctor"};
            Profession Lawyer = new Profession { ProfessionName = "Lawyer" };
            Profession Artist = new Profession { ProfessionName = "Artist" };
            Profession IT = new Profession { ProfessionName = "IT-Consultant" };
            Skill Medicine = new Skill {Id = 1, SkillName = "Medicine"};
            Skill Laws = new Skill {Id = 2, SkillName = "Laws"};
            Skill Art = new Skill {Id = 3, SkillName = "Art"};
            Skill Java = new Skill { Id = 3, SkillName = "Java" };
            Education Liu = new Education { Creator = 1, Description = "Studied medicine", EducationName = "Medicine", StartYear = new DateTime(2007, 01, 12), EndYear = new DateTime(2012, 01, 16), SchoolName = "Uppsala Universitet" };
            Education Oru = new Education { Creator = 3, Description = "Studied law", EducationName = "Legal studies", StartYear = new DateTime(2009, 09, 08), EndYear = new DateTime(2012, 01, 08), SchoolName = "Örebro Universitet" };
            Education Oru2 = new Education { Creator = 3, Description = "Studied social studies", EducationName = "Social studies", StartYear = new DateTime(2004, 09, 06), EndYear = new DateTime(2007, 01, 10), SchoolName = "Örebro Universitet" };
            Education Skara = new Education {  Creator = 2, Description = "Studied arts and crafts", EducationName = "Art", StartYear = new DateTime(1999, 09, 01), EndYear = new DateTime(2001, 01, 08), SchoolName = "Skara Folkhögskola" };
            Education OruIT = new Education { Creator = 4, Description = "Had the time of my life!", EducationName = "Software development", StartYear = new DateTime(2011, 09, 01), EndYear = new DateTime(2013, 01, 08), SchoolName = "Örebro Universitet" };
            PreviousExperience UppDoctor = new PreviousExperience { Creator = 1, Description = "Did doctor stuff", StartYear = new DateTime(2012, 02, 27), EndYear = new DateTime(2017, 12, 01), WorkplaceName = "Uppsala Sjukhus", WorkplaceTitle = "General Practitioner" };
            PreviousExperience OreKommun = new PreviousExperience { Creator = 3, Description = "Improved city life", StartYear = new DateTime(2007, 02, 01), EndYear = new DateTime(2009, 08, 01), WorkplaceName = "Örebro Kommun", WorkplaceTitle = "City consultant" };
            PreviousExperience Museum = new PreviousExperience { Creator = 2, Description = "Booked exhibiotns etc.", StartYear = new DateTime(2005, 04, 10), EndYear = new DateTime(2009, 10, 01), WorkplaceName = "Enköping Museum", WorkplaceTitle = "Exhibition planner" };
            PreviousExperience Mojang = new PreviousExperience { Creator = 4, Description = "Developed world generation improvement", StartYear = new DateTime(2014, 09, 13), EndYear = new DateTime(2020, 12, 31), WorkplaceName = "Mojang", WorkplaceTitle = "Developer" };
            Project Proj1 = new Project{ Creator = "user1", Title = "Hospital improvements", Description = "The hospital staff are hard at work at improving our facilities.", ImagePath = "/hospital.jpg" };
            Project Proj2 = new Project { Creator = "user1", Title = "Flu season training", Description = "Our staff needs to go through additional training for the upcoming flu season.", ImagePath = "/flu.jpg" };
            Project Proj3 = new Project { Creator = "user2", Title = "Art Exhibition", Description = "Showing my Art to the world.", ImagePath = "/exhibition.jpg" };
            Project Proj4 = new Project { Creator = "user3", Title = "For a better world", Description = "A project aimed at teaching people their rights in society.", ImagePath = "/justice.jpg" };
            Project Proj5 = new Project { Creator = "user4", Title = "Python 101", Description = "Crash course in python over the weekend.", ImagePath = "/python.jpg" };

            context.Professions.AddOrUpdate(
                p => p.ProfessionName,
                new Profession { ProfessionName = "Unemployed" },
                new Profession { ProfessionName = "Realtor" },
                Lawyer,
                Doctor,
                Artist,
                IT,
                new Profession { ProfessionName = "Teacher" },
                new Profession { ProfessionName = "Store salesman" },
                new Profession { ProfessionName = "Entrepreneur" },
                new Profession { ProfessionName = "Economist" },
                new Profession { ProfessionName = "Chef" },
                new Profession { ProfessionName = "Chaffeur"},
                new Profession { ProfessionName = "Carpenter" },
                new Profession { ProfessionName = "Hotel services" },
                new Profession { ProfessionName = "Receptionist" },
                new Profession { ProfessionName = "Engineer" },
                new Profession { ProfessionName = "Other" }
            );
            context.Users.AddOrUpdate(
                u => u.UserName,
                new ApplicationUser { Id = "user1", Email = "rikard.eriksson@gmail.com", EmailConfirmed = false, PasswordHash = "!secure1password", PhoneNumber = "0705000333", PhoneNumberConfirmed = false, LockoutEnabled = true, LockoutEndDateUtc = null, AccessFailedCount = 0, UserName = "rikard.eriksson@gmail.com" },
                            new ApplicationUser { Id = "user2", Email = "stefan.larsson@hotmail.com", EmailConfirmed = false, PasswordHash = "!secure1password", PhoneNumber = "0738430322", PhoneNumberConfirmed = false, LockoutEnabled = true, LockoutEndDateUtc = null, AccessFailedCount = 0, UserName = "stefan.larsson@hotmail.com" },
                            new ApplicationUser { Id = "user3", Email = "frida.lindblom@hotmail.se", EmailConfirmed = false, PasswordHash = "!secure1password", PhoneNumber = "0768436654", PhoneNumberConfirmed = false, LockoutEnabled = true, LockoutEndDateUtc = null, AccessFailedCount = 0, UserName = "frida.lindblom@hotmail.se" },
                             new ApplicationUser { Id = "user4", Email = "kim.hero@yahoo.com", EmailConfirmed = false, PasswordHash = "!secure1password", PhoneNumber = "0701237654", PhoneNumberConfirmed = false, LockoutEnabled = true, LockoutEndDateUtc = null, AccessFailedCount = 0, UserName = "anna.hero@yahoo.com" }
            );

            context.Skills.AddOrUpdate(Medicine, Laws, Art, Java);

            List<Skill> user1Skills = new List<Skill>();
            user1Skills.Add(Medicine);
            List<Skill> user2Skills = new List<Skill>();
            user2Skills.Add(Art);
            List<Skill> user3Skills = new List<Skill>();
            user3Skills.Add(Laws);
            List<Skill> user4Skills = new List<Skill>();
            user4Skills.Add(Java);
            user4Skills.Add(Art);

            context.Educations.AddOrUpdate(Liu, Oru, Oru2, Skara, OruIT);
            List<Education> user1Edu = new List<Education>();
            user1Edu.Add(Liu);
            List<Education> user2Edu = new List<Education>();
            user2Edu.Add(Skara);
            List<Education> user3Edu = new List<Education>();
            user3Edu.Add(Oru2);
            user3Edu.Add(Oru);
            List<Education> user4Edu = new List<Education>();
            user4Edu.Add(OruIT);

            context.PreviousExperiences.AddOrUpdate(UppDoctor, OreKommun, Museum, Mojang);
            List<PreviousExperience> user1Prev = new List<PreviousExperience>();
            user1Prev.Add(UppDoctor);
            List<PreviousExperience> user2Prev = new List<PreviousExperience>();
            user2Prev.Add(Museum);
            List<PreviousExperience> user3Prev = new List<PreviousExperience>();
            user3Prev.Add(OreKommun);
            List<PreviousExperience> user4Prev = new List<PreviousExperience>();
            user4Prev.Add(Mojang);

            context.Projects.AddOrUpdate(Proj1);
            List<Project> user1Proj = new List<Project>();
            user1Proj.Add(Proj1);
            user1Proj.Add(Proj2);
            List<Project> user2Proj = new List<Project>();
            user2Proj.Add(Proj3);
            List<Project> user3Proj = new List<Project>();
            user3Proj.Add(Proj4);
            List<Project> user4Proj = new List<Project>();
            user4Proj.Add(Proj5);
            user4Proj.Add(Proj4);




            context.CVs.AddOrUpdate(
                c => c.FirstName,
                new CV { Id = 1, FirstName = "Rikard", LastName = "Eriksson", Visits = 0, Email = "rikard.eriksson@gmail.com", PhoneNumber = "0705000333", Adress = "Köpmangatan 1, Örebro", Gender = "Male", Profession = 4, BirthDate = new DateTime(1989, 05, 13), Creator = "user1", ImagePath = "/rikarderiksson.jpg", Private = false, Deactivated = false, Bio = "This is me", UserProfession = Doctor, Skills = user1Skills, Educations = user1Edu, PreviousExperience = user1Prev, Projects = user1Proj},
                new CV { Id = 1, FirstName = "Stefan", LastName = "Larsson", Visits = 0, Email = "stefan.larsson@hotmail.com", PhoneNumber = "0738430322", Adress = "Lilla Svängen 4, Linköping", Gender = "Male", Profession = 5, BirthDate = new DateTime(1975, 01, 22), Creator = "user2", ImagePath = "/stefanlarsson.jpg", Private = true, Deactivated = false, Bio = "My passion is art.", UserProfession = Artist, Skills = user2Skills, Educations = user2Edu, PreviousExperience = user2Prev, Projects = user2Proj},
                new CV { Id = 1, FirstName = "Frida", LastName = "Lindblom", Visits = 0, Email = "frida.lindblom@hotmail.se", PhoneNumber = "0768436654", Adress = "Paradgatan 42, Sala", Gender = "Female", Profession = 3, BirthDate = new DateTime(1991, 08, 08), Creator = "user3", ImagePath = "/fridalindblom.jpg", Private = false, Deactivated = false, Bio = "Call me if you need help.", UserProfession = Lawyer, Skills = user3Skills, Educations = user3Edu, PreviousExperience = user3Prev, Projects = user3Proj},
                new CV { Id = 1, FirstName = "Kim", LastName = "Héro", Visits = 0, Email = "kim.hero@yahoo.com", PhoneNumber = "0701237654", Adress = "Strandgatan 12, Västerås", Gender = "Unspecified", Profession = 6, BirthDate = new DateTime(1986, 05, 30), Creator = "user4", ImagePath = "/kimhero.jpg", Private = false, Deactivated = false, Bio = "Im at your service!", UserProfession = IT, Skills = user4Skills, Educations = user4Edu, PreviousExperience = user4Prev, Projects = user4Proj}
                );

         
         

        }
    }
}
