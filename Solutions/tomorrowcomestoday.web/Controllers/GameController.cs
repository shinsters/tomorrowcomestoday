namespace TomorrowComesToday.Web.Controllers
{
    using System.Web.Mvc;

    public class GameController : Controller
    {
        /// <summary>
        /// The home controller is for the front login page, shouldn't be used for anything outside of that
        /// </summary>
        public GameController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
