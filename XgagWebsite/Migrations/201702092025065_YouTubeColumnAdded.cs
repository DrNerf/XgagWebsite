namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YouTubeColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "YouTubeLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "YouTubeLink");
        }
    }
}
