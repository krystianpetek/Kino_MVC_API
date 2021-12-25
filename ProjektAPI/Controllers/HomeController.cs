using Microsoft.AspNetCore.Mvc;

namespace ProjektAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AktualneFilmy()
        {
            return View();
        }
    }
}
