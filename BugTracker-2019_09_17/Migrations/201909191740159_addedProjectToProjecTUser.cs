namespace BugTracker_2019_09_17.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedProjectToProjecTUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProjectUsers", "ProjectId");
            AddForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectUsers", new[] { "ProjectId" });
        }
    }
}
