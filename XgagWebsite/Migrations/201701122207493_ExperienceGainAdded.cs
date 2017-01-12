namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExperienceGainAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonRanks", "ExperienceGain", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonRanks", "ExperienceGain");
        }
    }
}
