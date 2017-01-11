namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeopleVotesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonVotes",
                c => new
                    {
                        PersonVoteId = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonVoteId)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.UserDailyVotes",
                c => new
                    {
                        UserDailyVoteId = c.Int(nullable: false, identity: true),
                        VoteDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserDailyVoteId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDailyVotes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonVotes", "Person_PersonId", "dbo.People");
            DropIndex("dbo.UserDailyVotes", new[] { "User_Id" });
            DropIndex("dbo.PersonVotes", new[] { "Person_PersonId" });
            DropTable("dbo.UserDailyVotes");
            DropTable("dbo.PersonVotes");
            DropTable("dbo.People");
        }
    }
}
