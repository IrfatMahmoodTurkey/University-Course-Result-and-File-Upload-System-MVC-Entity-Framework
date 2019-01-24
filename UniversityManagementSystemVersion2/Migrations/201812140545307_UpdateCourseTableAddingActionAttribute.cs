namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCourseTableAddingActionAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ActionBy", c => c.String());
            AddColumn("dbo.Courses", "ActionDone", c => c.String());
            AddColumn("dbo.Courses", "ActionTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "ActionTime");
            DropColumn("dbo.Courses", "ActionDone");
            DropColumn("dbo.Courses", "ActionBy");
        }
    }
}
