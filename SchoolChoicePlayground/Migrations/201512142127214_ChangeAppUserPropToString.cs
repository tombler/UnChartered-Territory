namespace SchoolChoicePlayground.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAppUserPropToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "AspUser_Id" });
            AddColumn("dbo.Users", "AspUser", c => c.String(nullable: false));
            DropColumn("dbo.Users", "AspUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AspUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Users", "AspUser");
            CreateIndex("dbo.Users", "AspUser_Id");
            AddForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
