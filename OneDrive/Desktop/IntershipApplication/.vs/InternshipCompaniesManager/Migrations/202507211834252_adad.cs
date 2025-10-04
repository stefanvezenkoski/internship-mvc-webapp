namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "PortfolioLink", c => c.String(nullable: false));
            AddColumn("dbo.Applications", "CVFilePath", c => c.String(nullable: false));
            AddColumn("dbo.Applications", "Q1", c => c.String());
            AddColumn("dbo.Applications", "Q2", c => c.String());
            DropColumn("dbo.Applications", "MotivationLetter");
            DropColumn("dbo.Applications", "CV");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applications", "CV", c => c.String(nullable: false));
            AddColumn("dbo.Applications", "MotivationLetter", c => c.String(nullable: false));
            DropColumn("dbo.Applications", "Q2");
            DropColumn("dbo.Applications", "Q1");
            DropColumn("dbo.Applications", "CVFilePath");
            DropColumn("dbo.Applications", "PortfolioLink");
        }
    }
}
