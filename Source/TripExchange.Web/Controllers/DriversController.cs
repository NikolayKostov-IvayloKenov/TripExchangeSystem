namespace TripExchange.Web.Controllers
{
    using System;
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
        public IEnumerable<DriverViewModel> Get([FromUri]GetDriversBindingModel model)
        {
            const int ItemsPerPage = 10;

            // When called anonymously it returns the top 10 drivers with no paging and filtering
            if (!User.Identity.IsAuthenticated || model == null)
            {
                model = new GetDriversBindingModel { Page = 1, Username = null };
            }

            var data = this.Data.Users.All().Where(x => x.IsDriver).Select(DriverViewModel.FromApplicationUser);

            if (!string.IsNullOrEmpty(model.Username))
            {
                data = data.Where(driver => driver.Name.Contains(model.Username));
            }

            data = data.OrderByDescending(driver => driver.NumberOfTotalTrips).ThenBy(driver => driver.Name);

            if (model.Page > 1)
            {
                data = data.Skip(ItemsPerPage * (model.Page - 1));
            }

            data = data.Take(ItemsPerPage);

            return data.ToList();
        }
        
        [HttpGet]
        [Authorize]
        public DriverViewModel Get(string id)
        {
            return null;
        }
    }
}
