namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToTablePerHierarchy : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "People");
            AddColumn("dbo.People", "HireDate", c => c.DateTime());
            AddColumn("dbo.People", "Localization", c => c.String());
            AddColumn("dbo.People", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Client"));
            Sql("INSERT INTO dbo.\"People\"(\"Id\", \"FirstName\", \"LastName\", \"HireDate\",\"Localization\",\"Discriminator\")" +
                " SELECT \"Id\", \"FirstName\", \"LastName\", \"HireDate\", " +
                "\"Localization\", 'Employee' AS \"Discriminator\" FROM dbo.\"Employees\"");
            Sql("ALTER SEQUENCE dbo.\"Clients_Id_seq\" RENAME TO \"People_Id_seq\"");
            Sql("ALTER TABLE dbo.\"People\" RENAME CONSTRAINT \"PK_dbo.Clients\" TO \"PK_dbo.People\"");
            DropTable("dbo.Employees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(nullable: false),
                    FirstName = c.String(),
                    LastName = c.String(),
                    HireDate = c.DateTime(nullable: false),
                    Localization = c.String(),
                })
                .PrimaryKey(t => t.Id);

            DropForeignKey("dbo.Rentals", "ClientId", "dbo.People");
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.People", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.People", "Discriminator");
            DropColumn("dbo.People", "Localization");
            DropColumn("dbo.People", "HireDate");
            AddPrimaryKey("dbo.People", "Id");
            AddForeignKey("dbo.Rentals", "ClientId", "dbo.Clients", "Id");
            RenameTable(name: "dbo.People", newName: "Clients");
        }
    }
}
