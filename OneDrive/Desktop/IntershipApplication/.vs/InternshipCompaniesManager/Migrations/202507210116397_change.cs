namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "OpenPosition", c => c.String());
            DropColumn("dbo.Companies", "PositionType");
            DropColumn("dbo.Companies", "SpecificPosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "SpecificPosition", c => c.String());
            AddColumn("dbo.Companies", "PositionType", c => c.String());
            DropColumn("dbo.Companies", "OpenPosition");
        }
    }
}
