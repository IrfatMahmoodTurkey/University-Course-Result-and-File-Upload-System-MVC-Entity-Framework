namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeacherTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "ActionBy", c => c.String());
            AddColumn("dbo.Teachers", "ActionDone", c => c.String());
            AddColumn("dbo.Teachers", "ActionTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "ActionTime");
            DropColumn("dbo.Teachers", "ActionDone");
            DropColumn("dbo.Teachers", "ActionBy");
        }
    }
}
