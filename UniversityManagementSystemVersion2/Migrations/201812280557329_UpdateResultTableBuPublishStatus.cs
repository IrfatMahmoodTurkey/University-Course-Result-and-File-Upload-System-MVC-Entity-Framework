namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResultTableBuPublishStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentResults", "PublishStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentResults", "PublishStatus");
        }
    }
}
