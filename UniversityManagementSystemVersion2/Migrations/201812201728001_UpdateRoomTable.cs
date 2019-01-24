namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoomTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "RoomNo", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "RoomNo", c => c.String());
        }
    }
}
