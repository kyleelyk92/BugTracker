namespace BugTracker_2019_09_17.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeDateNullableInTicket : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
