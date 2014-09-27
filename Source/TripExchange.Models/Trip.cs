namespace TripExchange.Models
{
    using System;
    using System.Collections.Generic;

    public class Trip
    {
        private ICollection<ApplicationUser> passengers;

        public Trip()
        {
            this.Id = Guid.NewGuid();
            this.passengers = new HashSet<ApplicationUser>();
        }

        public Guid Id { get; set; }

        public DateTime DepartureTime { get; set; }
        
        public virtual ICollection<ApplicationUser> Passengers
        {
            get { return this.passengers; }
            set { this.passengers = value; }
        }
    }
}
