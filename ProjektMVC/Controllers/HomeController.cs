using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Models;
namespace ProjektAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
