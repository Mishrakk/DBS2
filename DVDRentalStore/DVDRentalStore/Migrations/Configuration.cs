namespace DVDRentalStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using DVDRentalStore.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Employee"))
            {
                roleManager.Create(new IdentityRole("Employee"));
            }
            if (!roleManager.RoleExists("Client"))
            {
                roleManager.Create(new IdentityRole("Client"));
            }
            var clients = new List<ApplicationUser>
            {
                new ApplicationUser{ UserName = "a.carson@example.com", Email = "a.carson@example.com",
                    Client = new Client{Id=1,FirstName="Carson",LastName="Alexander",Birthday=DateTime.Parse("2005-09-01")} },
                new ApplicationUser{ UserName = "m.alonso@example.com", Email = "m.alonso@example.com",
                    Client = new Client{Id=2,FirstName="Meredith",LastName="Alonso",Birthday=DateTime.Parse("2002-09-01")} },
                new ApplicationUser{ UserName = "a.anand@example.com", Email = "a.anand@example.com",
                    Client = new Client{Id=3,FirstName="Arturo",LastName="Anand",Birthday=DateTime.Parse("2003-09-01")} },
                new ApplicationUser{ UserName = "g.barzdukas@example.com", Email = "g.barzdukas@example.com",
                    Client = new Client{Id=4,FirstName="Gytis",LastName="Barzdukas",Birthday=DateTime.Parse("2002-09-01")} },
                new ApplicationUser{ UserName = "y.li@example.com", Email = "y.li@example.com",
                    Client = new Client{Id=5,FirstName="Yan",LastName="Li",Birthday=DateTime.Parse("2002-09-01")} },
                new ApplicationUser{ UserName = "p.justice@example.com", Email = "p.justice@example.com",
                    Client = new Client{Id=6,FirstName="Peggy",LastName="Justice",Birthday=DateTime.Parse("2001-09-01")} },
                new ApplicationUser{ UserName = "l.norman@example.com", Email = "l.norman@example.com",
                    Client = new Client{Id=7,FirstName="Laura",LastName="Norman",Birthday=DateTime.Parse("2003-09-01")} },
                new ApplicationUser{ UserName = "n.olivetto@example.com", Email = "n.olivetto@example.com",
                    Client = new Client{Id=8,FirstName="Nino",LastName="Olivetto",Birthday=DateTime.Parse("2005-09-01")} },
            };
            string defaultPassword = "pass123";
            foreach (var client in clients)
            {
                if (UserManager.FindByName(client.UserName) == null)
                {
                    UserManager.Create(client, defaultPassword);
                    UserManager.AddToRole(client.Id, "Client");
                }
            }

            var rentals = new List<Rental>
            {
                new Rental { RentDate=new DateTime(2020, 01, 15, 10, 0, 0), Client=clients[0].Client, ClientId=clients[0].Client.Id,
                    Copy=copies[0], CopyId=copies[0].ID},
                new Rental { RentDate=new DateTime(2019, 01, 15, 10, 0, 0), Client=clients[0].Client, ClientId=clients[0].Client.Id,
                    Copy=copies[0], CopyId=copies[0].ID}
            };
            rentals.ForEach(r => context.Rentals.AddOrUpdate(obj => new { obj.ClientId, obj.CopyId, obj.RentDate }, r));
            context.SaveChanges();

            var employees = new List<ApplicationUser>
            {
                new ApplicationUser{ UserName = "super.admin@example.com", Email = "super.admin@example.com",
                    Employee = new Employee{Id=1,FirstName="Super",LastName="Admin",HireDate=DateTime.Parse("1970-01-01")} },
            };
            foreach (var employee in employees)
            {
                if (UserManager.FindByName(employee.UserName) == null)
                {
                    UserManager.Create(employee, defaultPassword);
                    UserManager.AddToRole(employee.Id, "Employee");
                }
            }
        }
    }
}
