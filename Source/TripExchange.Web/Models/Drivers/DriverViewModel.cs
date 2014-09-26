namespace TripExchange.Web.Models.Drivers
{
    public class DriverViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int NumberOfUpcomingTrips { get; set; }

        public int NumberOfTotalTrips { get; set; }
    }
}
