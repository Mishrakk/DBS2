namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePerType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(nullable: false),
                    HireDate = c.DateTime(nullable: false),
                    Localization = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            CreateTable(
                "dbo.Clients",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Birthday = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            Sql("INSERT INTO dbo.\"Clients\" SELECT \"Id\", \"Birthday\" FROM dbo.\"People\" " +
                "WHERE \"Discriminator\" = 'Client'");
            Sql("INSERT INTO dbo.\"Employees\" SELECT \"Id\", \"HireDate\", \"Localization\" FROM dbo.\"People\" " +
                "WHERE \"Discriminator\" = 'Employee'");
            DropColumn("dbo.People", "Discriminator");
            DropColumn("dbo.People", "Localization");
            DropColumn("dbo.People", "HireDate");
            DropForeignKey("dbo.Rentals", "ClientId", "dbo.People");
            AddForeignKey("dbo.Rentals", "ClientId", "dbo.Clients", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "ClientId", "dbo.Clients");
            AddForeignKey("dbo.Rentals", "ClientId", "dbo.People", "Id");
            AddColumn("dbo.People", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Client"));
            AddColumn("dbo.People", "Localization", c => c.String());
            AddColumn("dbo.People", "HireDate", c => c.DateTime());
            AddColumn("dbo.People", "Birthday", c => c.DateTime());
            Sql("UPDATE dbo.\"People\"  SET \"Birthday\" = c.\"Birthday\" " +
                "FROM dbo.\"Clients\" c WHERE c.\"Id\" = \"People\".\"Id\"");
            Sql("UPDATE dbo.\"People\" SET \"Localization\" = e.\"Localization\", \"HireDate\" = e.\"HireDate\" " +
                "FROM dbo.\"Employees\" e WHERE e.\"Id\" = \"People\".\"Id\"");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
        }
    }
}
