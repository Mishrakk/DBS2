using System.Data.Entity;
using DVDRentalStore.Models;

namespace DVDRentalStore.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("DbConnString") { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
    }
}