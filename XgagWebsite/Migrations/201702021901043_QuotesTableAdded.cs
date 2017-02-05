namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuotesTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        QuoteId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Author_PersonId = c.Int(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuoteId)
                .ForeignKey("dbo.People", t => t.Author_PersonId)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Author_PersonId)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotes", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quotes", "Author_PersonId", "dbo.People");
            DropIndex("dbo.Quotes", new[] { "Owner_Id" });
            DropIndex("dbo.Quotes", new[] { "Author_PersonId" });
            DropTable("dbo.Quotes");
        }
    }
}
