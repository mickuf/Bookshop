namespace Bookshop.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Authors", "Surname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false, maxLength: 17));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Authors", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Authors", "Name", c => c.String(nullable: false));
        }
    }
}
