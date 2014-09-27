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
