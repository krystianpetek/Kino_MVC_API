using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
    public class LoginController : Controller
    {
        private APIDatabaseContext _context;
        public LoginController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UzytkownikModel> model = _context.Login.ToList();
            if (model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
