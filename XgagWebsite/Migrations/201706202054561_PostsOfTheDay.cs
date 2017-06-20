namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsOfTheDay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostOfTheDays",
                c => new
                    {
                        PostOfTheDayId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Post_PostId = c.Int(),
                    })
                .PrimaryKey(t => t.PostOfTheDayId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .Index(t => t.Post_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostOfTheDays", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.PostOfTheDays", new[] { "Post_PostId" });
            DropTable("dbo.PostOfTheDays");
        }
    }
}
