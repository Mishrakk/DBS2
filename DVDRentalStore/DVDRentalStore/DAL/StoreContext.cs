using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DVDRentalStore.Models;

namespace DVDRentalStore.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("DbConnString") 
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreContext, DVDRentalStore.Migrations.Configuration>());
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Client>().Map(m => { m.MapInheritedProperties(); m.ToTable("Clients"); });
            modelBuilder.Entity<Employee>().Map(e => { e.MapInheritedProperties(); e.ToTable("Employees"); });
        }
    }
}