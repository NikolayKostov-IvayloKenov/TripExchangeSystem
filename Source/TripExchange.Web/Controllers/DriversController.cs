namespace TripExchange.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using TripExchange.Data;
    using TripExchange.Web.Models.Drivers;

    public class DriversController : BaseApiController
    {
        public DriversController()
            : this(new TripExchangeData())
        {
        }

        public DriversController(ITripExchangeData data)
            : base(data)
        {
        }

        [HttpGet]
        public IEnumerable<DriverViewModel> Get()
        {
            var query = this.Data.Users.All().Where(x => x.IsDriver);

            var data = query.Select(user => new DriverViewModel
                                                {
                                                    Id = user.Id,
                                                    Name = user.UserName,
                                                    NumberOfTotalTrips = 0, // TODO: Implement
                                                    NumberOfUpcomingTrips = 0, // TODO: Implement
                                                });
            data = data.OrderByDescending(driver => driver.NumberOfTotalTrips).ThenBy(driver => driver.Name);
            return data.ToList();
        }

        [HttpGet]
        [Authorize]
        public DriverViewModel Get(string driverId)
        {
            return null;
        }
    }
}
