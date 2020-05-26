using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DVDRentalStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DVDRentalStore.DAL
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext() : base("DbConnString") 
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreContext, DVDRentalStore.Migrations.Configuration>());
        }
        public static StoreContext Create()
        {
            return new StoreContext();
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}