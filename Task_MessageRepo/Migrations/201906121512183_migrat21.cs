namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat21 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}
