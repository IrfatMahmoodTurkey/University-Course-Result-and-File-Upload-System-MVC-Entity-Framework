namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignmentOrReportTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentOrReports", "Url", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentOrReports", "Url");
        }
    }
}
