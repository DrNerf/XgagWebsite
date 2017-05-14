namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChitChatTweaks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChitChats", "DateTimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ChitChatVotes", "Voter_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ChitChatVotes", "Voter_Id");
            AddForeignKey("dbo.ChitChatVotes", "Voter_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChitChatVotes", "Voter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ChitChatVotes", new[] { "Voter_Id" });
            DropColumn("dbo.ChitChatVotes", "Voter_Id");
            DropColumn("dbo.ChitChats", "DateTimeCreated");
        }
    }
}
