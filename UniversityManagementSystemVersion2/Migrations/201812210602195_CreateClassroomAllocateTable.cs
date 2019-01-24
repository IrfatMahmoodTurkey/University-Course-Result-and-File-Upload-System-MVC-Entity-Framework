namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateClassroomAllocateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassroomAllocates",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.CourseId)
                .Index(t => t.DepartmentId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassroomAllocates", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassroomAllocates", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassroomAllocates", "CourseId", "dbo.Courses");
            DropIndex("dbo.ClassroomAllocates", new[] { "RoomId" });
            DropIndex("dbo.ClassroomAllocates", new[] { "DepartmentId" });
            DropIndex("dbo.ClassroomAllocates", new[] { "CourseId" });
            DropTable("dbo.ClassroomAllocates");
        }
    }
}
