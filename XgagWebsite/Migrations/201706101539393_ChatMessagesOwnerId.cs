namespace XgagWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatMessagesOwnerId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChatMessages", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.ChatMessages", name: "IX_Owner_Id", newName: "IX_OwnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChatMessages", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.ChatMessages", name: "OwnerId", newName: "Owner_Id");
        }
    }
}
