namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignmentTableUrl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AssignmentOrReports", "Url", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AssignmentOrReports", "Url", c => c.String(nullable: false));
        }
    }
}
