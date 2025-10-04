namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMotivationAndCVToApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "MotivationLetter", c => c.String(nullable: false));
            AddColumn("dbo.Applications", "CV", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "CV");
            DropColumn("dbo.Applications", "MotivationLetter");
        }
    }
}
