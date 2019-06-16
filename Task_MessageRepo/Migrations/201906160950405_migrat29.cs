namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUserId = c.String(),
                        Mess = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
