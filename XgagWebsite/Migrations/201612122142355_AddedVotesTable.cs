namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Post_PostId = c.Int(),
                        Voter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.Voter_Id)
                .Index(t => t.Post_PostId)
                .Index(t => t.Voter_Id);
            
            DropColumn("dbo.Posts", "Score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Score", c => c.Int(nullable: false));
            DropForeignKey("dbo.Votes", "Voter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "Post_PostId", "dbo.Posts");
            DropIndex("dbo.Votes", new[] { "Voter_Id" });
            DropIndex("dbo.Votes", new[] { "Post_PostId" });
            DropTable("dbo.Votes");
        }
    }
}
