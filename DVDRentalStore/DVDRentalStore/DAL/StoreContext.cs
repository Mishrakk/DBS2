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
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
    }
}