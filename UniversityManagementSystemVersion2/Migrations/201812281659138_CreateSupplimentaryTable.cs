namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSupplimentaryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supplimentaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Session = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        FileUrl = c.String(),
                        State = c.String(),
                        ActionBy = c.String(),
                        ActionDone = c.String(),
                        ActionTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supplimentaries", "CourseId", "dbo.Courses");
            DropIndex("dbo.Supplimentaries", new[] { "CourseId" });
            DropTable("dbo.Supplimentaries");
        }
    }
}
