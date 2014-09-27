namespace TripExchange.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "SPA Exam 2014 API";

            return this.View();
        }
    }
}
