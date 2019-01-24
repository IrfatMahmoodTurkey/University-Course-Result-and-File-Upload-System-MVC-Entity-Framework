namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStudentTableWithProfilePictureAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "ProfilePicture");
        }
    }
}
