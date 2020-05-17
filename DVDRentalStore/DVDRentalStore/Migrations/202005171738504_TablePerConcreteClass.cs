namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablePerConcreteClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "Id", "dbo.People");
            DropForeignKey("dbo.Employees", "Id", "dbo.People");
            RenameColumn("dbo.Clients", "Id", "OldId");
            AddColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: true));
            RenameColumn("dbo.Employees", "Id", "OldId");
            AddColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: true));
            Sql("UPDATE dbo.\"Clients\" SET \"Id\" = \"OldId\"");
            Sql("UPDATE dbo.\"Employees\" SET \"Id\" = \"OldId\"");
            DropForeignKey("dbo.Rentals", "FK_dbo.Rentals_dbo.Clients_ClientId");
            DropColumn("dbo.Clients", "OldId");
            DropColumn("dbo.Employees", "OldId");
            AddPrimaryKey("dbo.Clients", "Id");
            AddPrimaryKey("dbo.Employees", "Id");
            AddForeignKey("dbo.Rentals", "ClientId", "dbo.Clients", "Id");
            AddColumn("dbo.Clients", "FirstName", c => c.String());
            AddColumn("dbo.Clients", "LastName", c => c.String());
            AddColumn("dbo.Employees", "FirstName", c => c.String());
            AddColumn("dbo.Employees", "LastName", c => c.String());
            Sql("UPDATE dbo.\"Clients\"  SET \"FirstName\" = p.\"FirstName\", \"LastName\" = p.\"LastName\" " +
                "FROM dbo.\"People\" p WHERE p.\"Id\" = \"Clients\".\"Id\"");
            Sql("UPDATE dbo.\"Employees\"  SET \"FirstName\" = p.\"FirstName\", \"LastName\" = p.\"LastName\" " +
                "FROM dbo.\"People\" p WHERE p.\"Id\" = \"Employees\".\"Id\"");
            DropTable("dbo.People");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.People",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(),
                    LastName = c.String(),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO dbo.\"People\" SELECT \"Id\", \"FirstName\", \"LastName\" FROM dbo.\"Clients\" " +
                "UNION SELECT \"Id\", \"FirstName\", \"LastName\" FROM dbo.\"Employees\"");
            DropColumn("dbo.Employees", "LastName");
            DropColumn("dbo.Employees", "FirstName");
            DropColumn("dbo.Clients", "LastName");
            DropColumn("dbo.Clients", "FirstName");
            AlterColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: false));
            AlterColumn("dbo.Employees", "Id", c => c.Int(nullable: false, identity: false));
            AddForeignKey("dbo.Employees", "Id", "dbo.People", "Id");
            AddForeignKey("dbo.Clients", "Id", "dbo.People", "Id");
        }
    }
}
