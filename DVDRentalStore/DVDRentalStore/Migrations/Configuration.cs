namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using DVDRentalStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DVDRentalStore.DAL.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DVDRentalStore.DAL.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var movies = new List<Movie> {
                new Movie{Title="Anchorman", Year=2000 },
                new Movie{Title="Anchorman 2", Year=2001},
                new Movie{Title="Terminator", Year = 1993},
                new Movie{Title="Jurrasic Park", Year= 1999},
                new Movie{Title="The Lord of the Rings", Year= 1997},
            };
            movies.ForEach(movie => context.Movies.AddOrUpdate(obj => obj.Title, movie));
            context.SaveChanges();

            var copies = new List<Copy>
            {
                new Copy{ SerialNumber = "A001", Available=true, Movie=movies[0], MovieId=movies[0].MovieId},
                new Copy{ SerialNumber = "A002", Available=false, Movie=movies[0], MovieId=movies[0].MovieId},
                new Copy{ SerialNumber = "A003", Available=true, Movie=movies[0], MovieId=movies[0].MovieId},
                new Copy{ SerialNumber = "A004", Available=true, Movie=movies[1], MovieId=movies[1].MovieId},
                new Copy{ SerialNumber = "A005", Available=false, Movie=movies[2], MovieId=movies[2].MovieId},
            };
            copies.ForEach(c => context.Copies.AddOrUpdate(obj => obj.SerialNumber, c));
            context.SaveChanges();

            var clients = new List<Client>
            {
                new Client{FirstName="Carson",LastName="Alexander",Birthday=DateTime.Parse("2005-09-01")},
                new Client{FirstName="Meredith",LastName="Alonso",Birthday=DateTime.Parse("2002-09-01")},
                new Client{FirstName="Arturo",LastName="Anand",Birthday=DateTime.Parse("2003-09-01")},
                new Client{FirstName="Gytis",LastName="Barzdukas",Birthday=DateTime.Parse("2002-09-01")},
                new Client{FirstName="Yan",LastName="Li",Birthday=DateTime.Parse("2002-09-01")},
                new Client{FirstName="Peggy",LastName="Justice",Birthday=DateTime.Parse("2001-09-01")},
                new Client{FirstName="Laura",LastName="Norman",Birthday=DateTime.Parse("2003-09-01")},
                new Client{FirstName="Nino",LastName="Olivetto",Birthday=DateTime.Parse("2005-09-01")}
            };
            clients.ForEach(c => context.Clients.AddOrUpdate(obj => new { obj.FirstName, obj.LastName }, c));
            context.SaveChanges();

            var rentals = new List<Rental>
            {
                new Rental { RentDate=new DateTime(2020, 01, 15, 10, 0, 0), Client=clients[0], ClientId=clients[0].Id,
                    Copy=copies[0], CopyId=copies[0].ID},
                new Rental { RentDate=new DateTime(2019, 01, 15, 10, 0, 0), Client=clients[0], ClientId=clients[0].Id,
                    Copy=copies[0], CopyId=copies[0].ID}
            };
            rentals.ForEach(r => context.Rentals.AddOrUpdate(obj => new { obj.ClientId, obj.CopyId, obj.RentDate }, r));
            context.SaveChanges();
        }
    }
}
