namespace TomorrowComesToday.Web.Controllers
{
    using System.Web.Mvc;

    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    public class HomeController : Controller
    {
        /// <summary>
        /// The home controller is for the front login page, shouldn't be used for anything outside of that
        /// </summary>
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
