namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applications", "PortfolioLink", c => c.String());
            AlterColumn("dbo.Applications", "CVFilePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applications", "CVFilePath", c => c.String(nullable: false));
            AlterColumn("dbo.Applications", "PortfolioLink", c => c.String(nullable: false));
        }
    }
}
