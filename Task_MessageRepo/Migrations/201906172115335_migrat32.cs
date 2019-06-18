namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "DateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "DateTime", c => c.DateTime(nullable: false));
        }
    }
}
