namespace BugTracker_2019_09_17.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryingEnumOnStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Status");
        }
    }
}
