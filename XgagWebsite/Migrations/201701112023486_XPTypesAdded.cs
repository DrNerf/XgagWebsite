namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class XPTypesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "DownExperience", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.People", "UpExperience", c => c.Int(nullable: false, defaultValue: 0));
            DropColumn("dbo.People", "Experience");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Experience", c => c.Int(nullable: false));
            DropColumn("dbo.People", "UpExperience");
            DropColumn("dbo.People", "DownExperience");
        }
    }
}
