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
        public DbSet<Client> Clients { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}