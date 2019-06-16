namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateTime");
        }
    }
}
