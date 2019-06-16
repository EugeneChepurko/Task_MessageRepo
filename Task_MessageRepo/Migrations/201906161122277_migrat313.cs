namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat313 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Messages", "Id");
        }
    }
}
