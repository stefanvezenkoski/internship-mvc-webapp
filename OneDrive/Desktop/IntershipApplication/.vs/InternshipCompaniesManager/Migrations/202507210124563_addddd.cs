namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addddd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Position", c => c.String());
            DropColumn("dbo.Companies", "OpenPosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "OpenPosition", c => c.String());
            DropColumn("dbo.Companies", "Position");
        }
    }
}
