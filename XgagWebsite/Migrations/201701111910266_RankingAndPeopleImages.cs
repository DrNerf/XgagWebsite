namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RankingAndPeopleImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDailyVotes", "VoteType", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Image", c => c.Binary());
            DropColumn("dbo.UserDailyVotes", "VoteType");
        }
    }
}
