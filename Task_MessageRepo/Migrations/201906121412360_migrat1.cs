namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Date", c => c.DateTime(nullable: false));
        }
    }
}
