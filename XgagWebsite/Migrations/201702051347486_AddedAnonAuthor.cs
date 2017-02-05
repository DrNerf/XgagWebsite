namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnonAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "AnonymousAuthor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "AnonymousAuthor");
        }
    }
}
