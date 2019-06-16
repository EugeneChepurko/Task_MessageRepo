namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat30 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "ApplicationUserId");
            AddForeignKey("dbo.Messages", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Messages", "ApplicationUserId", c => c.String());
        }
    }
}
