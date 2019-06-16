namespace Task_MessageRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastMessage", c => c.String());
            DropColumn("dbo.AspNetUsers", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Message", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastMessage");
        }
    }
}
