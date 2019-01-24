namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExamRoutineTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamRoutines", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExamRoutines", "DepartmentId");
            AddForeignKey("dbo.ExamRoutines", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamRoutines", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ExamRoutines", new[] { "DepartmentId" });
            DropColumn("dbo.ExamRoutines", "DepartmentId");
        }
    }
}
