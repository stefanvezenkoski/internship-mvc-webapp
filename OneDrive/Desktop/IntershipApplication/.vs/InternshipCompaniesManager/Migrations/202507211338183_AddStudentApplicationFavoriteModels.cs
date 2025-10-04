namespace InternshipCompaniesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentApplicationFavoriteModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        AppliedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.StudentId);
            
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
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Favorites", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Applications", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Applications", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Favorites", new[] { "StudentId" });
            DropIndex("dbo.Favorites", new[] { "CompanyId" });
            DropIndex("dbo.Applications", new[] { "StudentId" });
            DropIndex("dbo.Applications", new[] { "CompanyId" });
            DropTable("dbo.Favorites");
            DropTable("dbo.Students");
            DropTable("dbo.Applications");
        }
    }
}
