namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat25 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mess = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
