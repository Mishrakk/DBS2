using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DVDRentalStore.Models;

namespace DVDRentalStore.DAL
{
    public class StoreInitializer : DropCreateDatabaseAlways<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var movies = new List<Movie> {
                new Movie { Title="Anchorman", Year=2000},
                new Movie { Title = "Anchorman 2", Year = 2001},
                new Movie { Title="Terminator", Year= 1993},
                new Movie { Title="Jurrasic Park", Year= 1999},
                new Movie { Title="The Lord of the Rings", Year= 1997},
            };
            movies.ForEach(movie => context.Movies.Add(movie));
            context.SaveChanges();
        }
    }
}