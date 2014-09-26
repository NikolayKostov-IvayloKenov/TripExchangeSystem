namespace TripExchange.Web.Controllers
{
    using TripExchange.Data;

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
    }
}