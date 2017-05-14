namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChitChatTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChitChats",
                c => new
                    {
                        ChitChatId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DangerType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChitChatId);
            
            CreateTable(
                "dbo.ChitChatVotes",
                c => new
                    {
                        ChitChatVoteId = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        ChitChat_ChitChatId = c.Int(),
                    })
                .PrimaryKey(t => t.ChitChatVoteId)
                .ForeignKey("dbo.ChitChats", t => t.ChitChat_ChitChatId)
                .Index(t => t.ChitChat_ChitChatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChitChatVotes", "ChitChat_ChitChatId", "dbo.ChitChats");
            DropIndex("dbo.ChitChatVotes", new[] { "ChitChat_ChitChatId" });
            DropTable("dbo.ChitChatVotes");
            DropTable("dbo.ChitChats");
        }
    }
}
