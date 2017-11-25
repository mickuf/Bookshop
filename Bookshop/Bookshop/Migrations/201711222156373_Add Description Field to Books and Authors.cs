namespace Bookshop.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDescriptionFieldtoBooksandAuthors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "Description", c => c.String(nullable: false, maxLength: 2000));
            AddColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Description");
            DropColumn("dbo.Authors", "Description");
        }
    }
}
