namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpenPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "OpenPosition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "OpenPosition");
        }
    }
}
