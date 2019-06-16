namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Mess = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Message_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Message_Id");
            AddForeignKey("dbo.AspNetUsers", "Message_Id", "dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Message_Id", "dbo.Messages");
            DropIndex("dbo.AspNetUsers", new[] { "Message_Id" });
            DropColumn("dbo.AspNetUsers", "Message_Id");
            DropTable("dbo.Messages");
        }
    }
}
