namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiSessionTokens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ApiSessionToken", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ApiSessionToken");
        }
    }
}
