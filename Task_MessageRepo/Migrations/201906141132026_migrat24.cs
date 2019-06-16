namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat24 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Message_Id", "dbo.Messages");
            DropIndex("dbo.AspNetUsers", new[] { "Message_Id" });
            AddColumn("dbo.Messages", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "Message_Id");
            DropColumn("dbo.Messages", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Message_Id", c => c.Int());
            DropForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropColumn("dbo.Messages", "User_Id");
            CreateIndex("dbo.AspNetUsers", "Message_Id");
            AddForeignKey("dbo.AspNetUsers", "Message_Id", "dbo.Messages", "Id");
        }
    }
}
