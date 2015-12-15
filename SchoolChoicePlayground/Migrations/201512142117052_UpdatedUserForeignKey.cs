namespace SchoolChoicePlayground.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "AspUser_Id" });
            AlterColumn("dbo.Users", "AspUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Users", "AspUser_Id");
            AddForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "AspUser_Id" });
            AlterColumn("dbo.Users", "AspUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "AspUser_Id");
            AddForeignKey("dbo.Users", "AspUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
