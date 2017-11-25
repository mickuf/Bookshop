namespace Bookshop.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Descriptionarenolongerrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "Description", c => c.String());
            AlterColumn("dbo.Books", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Authors", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
