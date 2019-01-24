namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCourseAssignedsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAssigneds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        State = c.String(),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId)
                .Index(t => t.CourseId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseAssigneds", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.CourseAssigneds", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseAssigneds", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssigneds", new[] { "CourseId" });
            DropTable("dbo.CourseAssigneds");
        }
    }
}
