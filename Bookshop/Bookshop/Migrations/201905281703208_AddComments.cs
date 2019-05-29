namespace Bookshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        Content = c.String(maxLength: 2000),
                        PublicationDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.BookComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Content = c.String(maxLength: 2000),
                        PublicationDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorComments", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.BookComments", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookComments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AuthorComments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookComments", new[] { "ApplicationUserId" });
            DropIndex("dbo.BookComments", new[] { "BookId" });
            DropIndex("dbo.AuthorComments", new[] { "ApplicationUserId" });
            DropIndex("dbo.AuthorComments", new[] { "AuthorId" });
            DropTable("dbo.BookComments");
            DropTable("dbo.AuthorComments");
        }
    }
}
