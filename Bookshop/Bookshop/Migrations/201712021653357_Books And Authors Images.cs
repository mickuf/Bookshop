namespace Bookshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksAndAuthorsImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "ImagePath", c => c.String());
            AddColumn("dbo.Books", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImagePath");
            DropColumn("dbo.Authors", "ImagePath");
        }
    }
}
