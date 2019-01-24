namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllocateTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.ClassRoomAllocations", new[] { "CourseId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DepartmentId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "RoomId" });
            DropTable("dbo.ClassRoomAllocations");
        }
        
        public override void Down()
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
            
            CreateIndex("dbo.ClassRoomAllocations", "RoomId");
            CreateIndex("dbo.ClassRoomAllocations", "DepartmentId");
            CreateIndex("dbo.ClassRoomAllocations", "CourseId");
            AddForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
