namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat331 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "UserName");
        }
    }
}
