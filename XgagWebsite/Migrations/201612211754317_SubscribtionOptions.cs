namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscribtionOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsSubscribedForNewPosts", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsSubscribedForComments", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsSubscribedForComments");
            DropColumn("dbo.AspNetUsers", "IsSubscribedForNewPosts");
        }
    }
}
