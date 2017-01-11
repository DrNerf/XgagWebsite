namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScoreAddedToRanking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonRanks", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonRanks", "Score");
        }
    }
}
