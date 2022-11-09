using Data.Models;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Professions.AddOrUpdate(
                p => p.ProfessionName,
                new Profession { ProfessionName = "Unemployed" },
                new Profession { ProfessionName = "Mäklare" },
                new Profession { ProfessionName = "Jurist" },
                new Profession { ProfessionName = "Läkare" },
                new Profession { ProfessionName = "IT-Konsult" },
                new Profession { ProfessionName = "Lärare" },
                new Profession { ProfessionName = "Butiksäljare" },
                new Profession { ProfessionName = "Entreprenör" },
                new Profession { ProfessionName = "Artist" },
                new Profession { ProfessionName = "Ekonom" }
            );
            //
        }
    }
}
