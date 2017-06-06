namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActivatedAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsActivated", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsActivated");
        }
    }
}
