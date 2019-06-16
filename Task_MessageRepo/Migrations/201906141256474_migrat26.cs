namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat26 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Mess = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropTable("dbo.Messages");
        }
    }
}
