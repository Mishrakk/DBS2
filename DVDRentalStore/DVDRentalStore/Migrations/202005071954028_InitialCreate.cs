namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Copies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Available = c.Boolean(nullable: false),
                        MovieId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Copies", "MovieId", "dbo.Movies");
            DropIndex("dbo.Copies", new[] { "MovieId" });
            DropTable("dbo.Movies");
            DropTable("dbo.Copies");
        }
    }
}
