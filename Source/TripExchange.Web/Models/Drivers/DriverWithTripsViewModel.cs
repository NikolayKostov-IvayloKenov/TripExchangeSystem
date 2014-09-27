namespace TripExchange.Web.Models.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using TripExchange.Models;
    using TripExchange.Web.Models.Trips;

    public class DriverWithTripsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int NumberOfUpcomingTrips { get; set; }

        public int NumberOfTotalTrips { get; set; }

        public IEnumerable<TripViewModel> Trips { get; set; }

        public static Expression<Func<ApplicationUser, DriverWithTripsViewModel>> FromApplicationUser(string currentUserUsername)
        {
            return
                user =>
                new DriverWithTripsViewModel
                    {
                        Id = user.Id,
                        Name = user.UserName,
                        NumberOfTotalTrips = user.Trips.AsQueryable().Count(),
                        NumberOfUpcomingTrips =
                            user.Trips.AsQueryable().Count(trip => trip.DepartureTime > DateTime.Now),
                        Trips = user.Trips.AsQueryable().Select(TripViewModel.FromTrip(currentUserUsername)),
                    };
        }
    }
}