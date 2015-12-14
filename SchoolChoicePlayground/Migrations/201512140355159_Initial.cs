namespace SchoolChoicePlayground.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false),
                        phoneNum = c.String(),
                        email = c.String(),
                        alerts = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
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
                "dbo.UserSchools",
                c => new
                    {
                        User_UserId = c.String(nullable: false, maxLength: 128),
                        School_SchoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.School_SchoolId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Schools", t => t.School_SchoolId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.School_SchoolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserSchools", "School_SchoolId", "dbo.Schools");
            DropForeignKey("dbo.UserSchools", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Schools", "SchoolId", "dbo.Addresses");
            DropIndex("dbo.UserSchools", new[] { "School_SchoolId" });
            DropIndex("dbo.UserSchools", new[] { "User_UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Schools", new[] { "SchoolId" });
            DropTable("dbo.UserSchools");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Alerts");
            DropTable("dbo.Users");
            DropTable("dbo.Schools");
            DropTable("dbo.Addresses");
        }
    }
}
