namespace UniversityManagementSystemVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSupplymentaryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplimentaries", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Supplimentaries", "FileName");
        }
    }
}
