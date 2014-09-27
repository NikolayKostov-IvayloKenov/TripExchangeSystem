namespace TripExchange.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TripExchange.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const int NumberOfUsers = 40;
            const int NumberOfTrips = 200;

            if (context.Users.Any())
            {
                return;
            }

            var random = new Random();

            var userStore = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(userStore);

            var users = new List<ApplicationUser>();

            for (var i = 1; i <= NumberOfUsers; i++)
            {
                var userName = string.Format("test{0:D2}@test.com", i);
                const string Password = "123456";
                var isDriver = i % 2 == 0;
                var car = isDriver ? string.Format("car {0}", i) : null;
                var user = new ApplicationUser
                               {
                                   UserName = userName,
                                   Email = userName,
                                   IsDriver = isDriver,
                                   Car = car,
                               };

                var identityResult = manager.Create(user, Password);
                if (identityResult.Succeeded)
                {
                    users.Add(user);
                }
            }

            for (var i = -NumberOfTrips / 2; i <= NumberOfTrips / 2; i++)
            {
                var trip = new Trip { DepartureTime = DateTime.Now.AddDays(i), };

                for (var j = 0; j < random.Next(0, 5); j++)
                {
                    trip.Passengers.Add(users[random.Next(0, users.Count)]);
                }

                context.Trips.Add(trip);
            }

            context.SaveChanges();
        }
    }
}
