namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePerHierarchy : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "People");
            AddColumn("dbo.People", "HireDate", c => c.DateTime());
            AddColumn("dbo.People", "Localization", c => c.String());
            AddColumn("dbo.People", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Client"));
            AlterColumn("dbo.People", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Birthday", c => c.DateTime(nullable: false));
            DropColumn("dbo.People", "Discriminator");
            DropColumn("dbo.People", "Localization");
            DropColumn("dbo.People", "HireDate");
            RenameTable(name: "dbo.People", newName: "Clients");
        }
    }
}
