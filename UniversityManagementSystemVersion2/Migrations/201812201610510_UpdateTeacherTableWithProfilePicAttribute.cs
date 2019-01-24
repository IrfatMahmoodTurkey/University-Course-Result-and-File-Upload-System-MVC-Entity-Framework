namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTeacherTableWithProfilePicAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "ProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "ProfilePicture");
        }
    }
}
