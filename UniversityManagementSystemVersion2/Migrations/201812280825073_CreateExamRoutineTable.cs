namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateExamRoutineTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamRoutines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Date = c.String(nullable: false),
                        FromTime = c.String(nullable: false),
                        ToTime = c.String(nullable: false),
                        Session = c.String(nullable: false),
                        State = c.String(),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamRoutines", "CourseId", "dbo.Courses");
            DropIndex("dbo.ExamRoutines", new[] { "CourseId" });
            DropTable("dbo.ExamRoutines");
        }
    }
}
