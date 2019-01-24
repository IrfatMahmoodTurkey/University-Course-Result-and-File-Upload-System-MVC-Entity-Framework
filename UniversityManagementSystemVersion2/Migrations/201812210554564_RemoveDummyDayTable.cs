namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDummyDayTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Day = c.String(),
                        FromTime = c.String(nullable: false),
                        ToTime = c.String(nullable: false),
                        State = c.String(),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Days");
        }
        
        public override void Down()
        {
            DropTable("dbo.ClassRoomAllocations");
        }
    }
}
