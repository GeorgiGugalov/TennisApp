using Microsoft.AspNetCore.Mvc;

namespace TennisApp.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
