namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentalModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RentDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        CopyId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Copies", t => t.CopyId, cascadeDelete: true)
                .Index(t => t.CopyId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "CopyId", "dbo.Copies");
            DropForeignKey("dbo.Rentals", "ClientId", "dbo.Clients");
            DropIndex("dbo.Rentals", new[] { "ClientId" });
            DropIndex("dbo.Rentals", new[] { "CopyId" });
            DropTable("dbo.Rentals");
        }
    }
}
