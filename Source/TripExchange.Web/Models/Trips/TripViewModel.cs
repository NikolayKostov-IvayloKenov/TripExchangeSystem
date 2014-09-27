namespace TripExchange.Web.Models.Trips
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using TripExchange.Models;

    public class TripViewModel
    {
        public string Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime DepartureDate { get; set; }

        public int NumberOfFreeSeats { get; set; }

        public bool IsMine { get; set; }

        public static Expression<Func<Trip, TripViewModel>> FromTrip(string currentUserUsername)
        {
            return
                trip =>
                new TripViewModel
                    {
                        Id = trip.Id.ToString(),
                        From = trip.From.Name,
                        To = trip.To.Name,
                        DepartureDate = trip.DepartureTime,
                        NumberOfFreeSeats = trip.AvailableSeats - trip.Passengers.Count,
                        IsMine = trip.Passengers.AsQueryable().Count(u => u.UserName == currentUserUsername) > 0,
                    };
        }
    }
}