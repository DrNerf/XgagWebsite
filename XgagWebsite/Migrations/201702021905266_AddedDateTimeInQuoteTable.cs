namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateTimeInQuoteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "DateTimeCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "DateTimeCreated");
        }
    }
}
