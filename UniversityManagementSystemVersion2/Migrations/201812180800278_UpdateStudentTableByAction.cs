namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentTableByAction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ActionBy", c => c.String());
            AddColumn("dbo.Students", "ActionDone", c => c.String());
            AddColumn("dbo.Students", "ActionTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "ActionTime");
            DropColumn("dbo.Students", "ActionDone");
            DropColumn("dbo.Students", "ActionBy");
        }
    }
}
