namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Visits = c.Int(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Adress = c.String(),
                        Gender = c.String(),
                        Profession = c.Int(nullable: false),
                        Bio = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Creator = c.String(),
                        Private = c.Boolean(nullable: false),
                        ImagePath = c.String(),
                        Deactivated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professions", t => t.Profession, cascadeDelete: true)
                .Index(t => t.Profession);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(nullable: false),
                        SchoolName = c.String(nullable: false),
                        EducationName = c.String(nullable: false),
                        Description = c.String(),
                        Creator = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sender = c.String(),
                        MessageText = c.String(),
                        SenderId = c.Int(nullable: false),
                        RecieverID = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        CV_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CV_Id)
                .Index(t => t.CV_Id);
            
            CreateTable(
                "dbo.PreviousExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.DateTime(nullable: false),
                        EndYear = c.DateTime(nullable: false),
                        WorkplaceName = c.String(nullable: false),
                        WorkplaceTitle = c.String(nullable: false),
                        Description = c.String(),
                        Creator = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Creator = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GitRepoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        html_url = c.String(),
                        CV_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CV_Id)
                .Index(t => t.CV_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Professions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfessionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectCVs",
                c => new
                    {
                        Project_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.CV_Id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.CV_Id);
            
            CreateTable(
                "dbo.SkillCVs",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        CV_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.CV_Id })
                .ForeignKey("dbo.Skills", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("dbo.CVs", t => t.CV_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.CV_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CVs", "Profession", "dbo.Professions");
            DropForeignKey("dbo.SkillCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.SkillCVs", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.GitRepoes", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.ProjectCVs", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.ProjectCVs", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.PreviousExperiences", "Creator", "dbo.CVs");
            DropForeignKey("dbo.Messages", "CV_Id", "dbo.CVs");
            DropForeignKey("dbo.Educations", "Creator", "dbo.CVs");
            DropIndex("dbo.SkillCVs", new[] { "CV_Id" });
            DropIndex("dbo.SkillCVs", new[] { "Skill_Id" });
            DropIndex("dbo.ProjectCVs", new[] { "CV_Id" });
            DropIndex("dbo.ProjectCVs", new[] { "Project_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.GitRepoes", new[] { "CV_Id" });
            DropIndex("dbo.PreviousExperiences", new[] { "Creator" });
            DropIndex("dbo.Messages", new[] { "CV_Id" });
            DropIndex("dbo.Educations", new[] { "Creator" });
            DropIndex("dbo.CVs", new[] { "Profession" });
            DropTable("dbo.SkillCVs");
            DropTable("dbo.ProjectCVs");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Professions");
            DropTable("dbo.Skills");
            DropTable("dbo.GitRepoes");
            DropTable("dbo.Projects");
            DropTable("dbo.PreviousExperiences");
            DropTable("dbo.Messages");
            DropTable("dbo.Educations");
            DropTable("dbo.CVs");
        }
    }
}
