namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoomTableWithActions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "ActionBy", c => c.String());
            AddColumn("dbo.Rooms", "ActionDone", c => c.String());
            AddColumn("dbo.Rooms", "ActionTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "ActionTime");
            DropColumn("dbo.Rooms", "ActionDone");
            DropColumn("dbo.Rooms", "ActionBy");
        }
    }
}
