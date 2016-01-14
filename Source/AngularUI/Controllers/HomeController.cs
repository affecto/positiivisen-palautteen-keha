using Microsoft.AspNet.Mvc;

namespace Affecto.PositiveFeedback.AngularUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}