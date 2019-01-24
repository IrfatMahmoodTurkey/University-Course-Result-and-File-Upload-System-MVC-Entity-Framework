namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCourseTableWithNameAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Name");
        }
    }
}
