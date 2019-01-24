namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAssignmentOrReportTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignmentOrReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Session = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Date = c.String(nullable: false),
                        UploadDate = c.String(),
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
            DropForeignKey("dbo.AssignmentOrReports", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AssignmentOrReports", "CourseId", "dbo.Courses");
            DropIndex("dbo.AssignmentOrReports", new[] { "StudentId" });
            DropIndex("dbo.AssignmentOrReports", new[] { "CourseId" });
            DropTable("dbo.AssignmentOrReports");
        }
    }
}
