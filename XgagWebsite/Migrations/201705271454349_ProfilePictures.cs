namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilePictures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePictureUrl", c => c.String(nullable: false, defaultValueSql: "'~/Content/Images/profile.svg'"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePictureUrl");
        }
    }
}
