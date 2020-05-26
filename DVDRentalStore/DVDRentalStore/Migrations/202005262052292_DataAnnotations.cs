namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.People", name: "FirstName", newName: "FName");
            RenameColumn(table: "dbo.People", name: "LastName", newName: "LName");
            AlterColumn("dbo.People", "FName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "LName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "LName", c => c.String());
            AlterColumn("dbo.People", "FName", c => c.String());
            RenameColumn(table: "dbo.People", name: "LName", newName: "LastName");
            RenameColumn(table: "dbo.People", name: "FName", newName: "FirstName");
        }
    }
}
