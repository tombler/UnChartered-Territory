namespace SchoolChoicePlayground.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSchoolUserKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSchools", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserSchools", new[] { "User_UserId" });
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.UserSchools");
            AddColumn("dbo.Users", "AspUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserSchools", "User_UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.UserSchools", new[] { "User_UserId", "School_SchoolId" });
            CreateIndex("dbo.Users", "AspUser_Id");
            CreateIndex("dbo.UserSchools", "User_UserId");
            AddForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserSchools", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSchools", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserSchools", new[] { "User_UserId" });
            DropIndex("dbo.Users", new[] { "AspUser_Id" });
            DropPrimaryKey("dbo.UserSchools");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.UserSchools", "User_UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Users", "AspUser_Id");
            AddPrimaryKey("dbo.UserSchools", new[] { "User_UserId", "School_SchoolId" });
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.UserSchools", "User_UserId");
            AddForeignKey("dbo.UserSchools", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
