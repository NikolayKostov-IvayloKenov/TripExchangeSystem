namespace TripExchange.Web.Controllers
{
    using TripExchange.Data;
    using TripExchange.Web.Models.Stats;

    public class StatsController : BaseApiController
    {
        public StatsController()
            : this(new TripExchangeData())
        {
        }

        public StatsController(ITripExchangeData data)
            : base(data)
        {
        }

        public StatsViewModel Get()
        {
            return new StatsViewModel();
        }
    }
}