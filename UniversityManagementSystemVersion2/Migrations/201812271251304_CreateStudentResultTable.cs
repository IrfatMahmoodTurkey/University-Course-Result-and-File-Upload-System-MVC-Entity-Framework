namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStudentResultTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Attendance = c.Int(nullable: false),
                        Assignement = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClassTest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MidTerm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalExam = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Point = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Session = c.String(nullable: false),
                        State = c.String(),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentResults", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentResults", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentResults", new[] { "StudentId" });
            DropIndex("dbo.StudentResults", new[] { "CourseId" });
            DropTable("dbo.StudentResults");
        }
    }
}
