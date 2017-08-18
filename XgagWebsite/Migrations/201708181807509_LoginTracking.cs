namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginTracking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Trace", c => c.String());
            AddColumn("dbo.AspNetUsers", "Browser", c => c.String());
            AddColumn("dbo.AspNetUsers", "BrowserVersion", c => c.String());
            AddColumn("dbo.AspNetUsers", "Platform", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime(nullable: true));
            AddColumn("dbo.AspNetUsers", "IPAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IPAddress");
            DropColumn("dbo.AspNetUsers", "LastLogin");
            DropColumn("dbo.AspNetUsers", "Platform");
            DropColumn("dbo.AspNetUsers", "BrowserVersion");
            DropColumn("dbo.AspNetUsers", "Browser");
            DropColumn("dbo.AspNetUsers", "Trace");
        }
    }
}
