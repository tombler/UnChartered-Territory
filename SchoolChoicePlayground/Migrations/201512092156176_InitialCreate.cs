namespace SchoolChoicePlayground.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Line1 = c.String(),
                        Line2 = c.String(),
                        city = c.String(),
                        state = c.Int(nullable: false),
                        zip = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        SchoolId = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        grades = c.String(),
                        phoneNum = c.String(),
                        lat = c.Double(nullable: false),
                        lng = c.Double(nullable: false),
                        addlInfo = c.String(maxLength: 200),
                        level = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        website = c.String(),
                    })
                .PrimaryKey(t => t.SchoolId)
                .ForeignKey("dbo.Addresses", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.SchoolAppUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        phoneNum = c.String(),
                        provider = c.String(),
                        email = c.String(),
                        alerts = c.Boolean(nullable: false),
                        RealUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.RealUser_Id)
                .Index(t => t.RealUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hometown = c.String(),
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        AlertId = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        dateToSend = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AlertId);
            
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
                "dbo.SchoolAppUserSchools",
                c => new
                    {
                        SchoolAppUser_UserId = c.Int(nullable: false),
                        School_SchoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolAppUser_UserId, t.School_SchoolId })
                .ForeignKey("dbo.SchoolAppUsers", t => t.SchoolAppUser_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.School_SchoolId, cascadeDelete: true)
                .Index(t => t.SchoolAppUser_UserId)
                .Index(t => t.School_SchoolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SchoolAppUserSchools", "School_SchoolId", "dbo.Schools");
            DropForeignKey("dbo.SchoolAppUserSchools", "SchoolAppUser_UserId", "dbo.SchoolAppUsers");
            DropForeignKey("dbo.SchoolAppUsers", "RealUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Schools", "SchoolId", "dbo.Addresses");
            DropIndex("dbo.SchoolAppUserSchools", new[] { "School_SchoolId" });
            DropIndex("dbo.SchoolAppUserSchools", new[] { "SchoolAppUser_UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SchoolAppUsers", new[] { "RealUser_Id" });
            DropIndex("dbo.Schools", new[] { "SchoolId" });
            DropTable("dbo.SchoolAppUserSchools");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Alerts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SchoolAppUsers");
            DropTable("dbo.Schools");
            DropTable("dbo.Addresses");
        }
    }
}
