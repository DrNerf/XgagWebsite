namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteTypeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "QuoteType", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "QuoteType");
        }
    }
}
