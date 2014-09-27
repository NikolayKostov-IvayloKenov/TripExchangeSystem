namespace TripExchange.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TripExchange.Data;
    using TripExchange.Models;
    using TripExchange.Web.Models.Trips;

    public class TripsController : BaseApiController
    {
        public TripsController()
            : this(new TripExchangeData())
        {
        }

        public TripsController(ITripExchangeData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.BadRequest("Invalid trip id!");
            }

            var currentUserName = User.Identity.Name;

            var tripId = new Guid(id);
            var tripData =
                this.Data.Trips.All()
                    .Where(trip => trip.Id == tripId)
                    .Select(TripViewModel.FromTrip(currentUserName))
                    .FirstOrDefault();

            if (tripData == null)
            {
                return this.BadRequest("Trip not found!");
            }

            return this.Ok(tripData);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Put(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            var tripId = new Guid(id);
            var trip =
                this.Data.Trips.All()
                    .Select(
                        x =>
                        new
                            {
                                x.Id,
                                x.DepartureTime,
                                x.AvailableSeats,
                                PassengersCount = x.Passengers.Count,
                                IsCurrentUserInTheTrip = x.Passengers.Any(user => user.Id == currentUserId)
                            })
                    .FirstOrDefault(x => x.Id == tripId);

            if (trip == null)
            {
                return this.BadRequest("Trip with given id not found!");
            }

            if (trip.DepartureTime <= DateTime.Now)
            {
                return this.BadRequest("Selected trip's departure date is in the past.");
            }

            if (trip.IsCurrentUserInTheTrip)
            {
                return this.BadRequest("You are already part of the trip!");
            }

            var tripRemainingSeats = trip.AvailableSeats - trip.PassengersCount;
            if (tripRemainingSeats < 1)
            {
                return this.BadRequest("There are no available seats in the given trip!");
            }

            var databaseTrip = this.Data.Trips.GetById(trip.Id);
            databaseTrip.Passengers.Add(currentUser);
            this.Data.SaveChanges();

            return this.Get(id);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(CreateTripBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var fromCity = this.Data.Cities.All().FirstOrDefault(city => city.Name == model.From);
            if (fromCity == null)
            {
                return this.BadRequest("\"From\" city does not exist!");
            }

            var toCity = this.Data.Cities.All().FirstOrDefault(city => city.Name == model.To);
            if (toCity == null)
            {
                return this.BadRequest("\"To\" city does not exist!");
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser == null)
            {
                return this.BadRequest("Invalid user token! Please login again!");
            }

            if (!currentUser.IsDriver)
            {
                return this.BadRequest("You should be driver to create trips!");
            }

            var trip = new Trip
                           {
                               AvailableSeats = (byte)(model.AvailableSeats + 1),
                               From = fromCity,
                               To = toCity,
                               DepartureTime = model.DepartureTime,
                               DriverId = currentUser.Id,
                           };

            trip.Passengers.Add(currentUser);

            this.Data.Trips.Add(trip);
            this.Data.SaveChanges();

            return this.Get(trip.Id.ToString());
        }
    }
}
