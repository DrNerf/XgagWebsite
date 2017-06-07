namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveAddedToPeople : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "IsActive");
        }
    }
}
