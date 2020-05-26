namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePerHierarchy : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "People");
            // Sequences and constraints have names changed, to better reflect new name of table
            Sql("ALTER SEQUENCE dbo.\"Clients_Id_seq\" RENAME TO \"People_Id_seq\"");
            Sql("ALTER TABLE dbo.\"People\" RENAME CONSTRAINT \"PK_dbo.Clients\" TO \"PK_dbo.People\"");
            Sql("ALTER TABLE dbo.\"Rentals\" RENAME CONSTRAINT \"FK_dbo.Rentals_dbo.Clients_ClientId\" TO \"FK_dbo.Rentals_dbo.People_ClientId\"");
            AddColumn("dbo.People", "HireDate", c => c.DateTime());
            AddColumn("dbo.People", "Localization", c => c.String());
            AddColumn("dbo.People", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Client"));
            AlterColumn("dbo.People", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Birthday", c => c.DateTime());
            DropColumn("dbo.People", "Discriminator");
            DropColumn("dbo.People", "Localization");
            DropColumn("dbo.People", "HireDate");
            RenameTable(name: "dbo.People", newName: "Clients");
            // Sequences and constraints have names changed, to better reflect new name of table
            Sql("ALTER SEQUENCE dbo.\"People_Id_seq\" RENAME TO \"Clients_Id_seq\"");
            Sql("ALTER TABLE dbo.\"People\" RENAME CONSTRAINT \"PK_dbo.People\" TO \"PK_dbo.Clients\"");
            Sql("ALTER TABLE dbo.\"Rentals\" RENAME CONSTRAINT \"FK_dbo.Rentals_dbo.People_ClientId\" TO \"FK_dbo.Rentals_dbo.Clients_ClientId\"");
        }
    }
}
