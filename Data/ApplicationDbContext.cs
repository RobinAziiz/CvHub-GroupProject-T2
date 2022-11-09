using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    public DbSet<Project> Projects { get; set; }
    public DbSet<CV> CVs { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<PreviousExperience> PreviousExperiences { get; set; }
    public DbSet<Skill> Skills { get; set; }

    public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());

            //Seed körs inte om SetInitialzer är av CreateDatabaseIfNotExists, och vi vill fylla databasen med lite testdata.
            //För att undgå det problemet har vi skapat en egen Initializer-klass som ärver från CreateDatabaseIfNotExists
            //som har sin egen overridden Seed-metod. 
            Database.SetInitializer(new DbInitializer());
     
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


}