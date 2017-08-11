namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NSFWAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsNSFW", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsNSFW");
        }
    }
}
