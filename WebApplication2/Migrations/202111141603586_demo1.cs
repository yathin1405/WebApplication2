namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class demo1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Flights", "returnTicket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "returnTicket", c => c.Boolean(nullable: false));
        }
    }
}
