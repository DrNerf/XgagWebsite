namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingRankingTablesAndPeopleExperience : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonRanks",
                c => new
                    {
                        PersonRankId = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        RankType = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonRankId)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.RankingHistoryItems",
                c => new
                    {
                        PersonRankingHistoryId = c.Int(nullable: false, identity: true),
                        RankingDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonRankingHistoryId);
            
            AddColumn("dbo.People", "Experience", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonRanks", "Person_PersonId", "dbo.People");
            DropIndex("dbo.PersonRanks", new[] { "Person_PersonId" });
            DropColumn("dbo.People", "Experience");
            DropTable("dbo.RankingHistoryItems");
            DropTable("dbo.PersonRanks");
        }
    }
}
