namespace TripExchange.Web.Controllers
{
    using TripExchange.Data;

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
    }
}