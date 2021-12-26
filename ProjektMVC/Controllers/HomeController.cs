using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Models;
using System.Collections.Generic;

namespace ProjektAPI.Controllers
{
    public class HomeController : Controller
    {
        private APIDatabaseContext _context;
        public HomeController(APIDatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AktualneFilmy()
        {
            List<FilmModel> x = new();
            x.AddRange(_context.Filmy);
            return View(x);
        }
    }
}
