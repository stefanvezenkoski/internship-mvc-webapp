namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applications", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Favorites", "StudentId", "dbo.Students");
            DropIndex("dbo.Applications", new[] { "StudentId" });
            DropIndex("dbo.Favorites", new[] { "StudentId" });
            AddColumn("dbo.Applications", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Favorites", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            CreateIndex("dbo.Applications", "UserId");
            CreateIndex("dbo.Favorites", "UserId");
            AddForeignKey("dbo.Applications", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Applications", "StudentId");
            DropColumn("dbo.Favorites", "StudentId");
            DropTable("dbo.Students");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(),
                        University = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Favorites", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.Applications", "StudentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applications", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Applications", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.Favorites", "UserId");
            DropColumn("dbo.Applications", "UserId");
            CreateIndex("dbo.Favorites", "StudentId");
            CreateIndex("dbo.Applications", "StudentId");
            AddForeignKey("dbo.Favorites", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Applications", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
