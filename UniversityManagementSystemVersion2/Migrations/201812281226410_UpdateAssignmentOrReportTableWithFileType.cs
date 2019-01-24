namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssignmentOrReportTableWithFileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentOrReports", "FileType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentOrReports", "FileType");
        }
    }
}
