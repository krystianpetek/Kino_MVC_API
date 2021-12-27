using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ProjektAPI.Models;
using System.Collections.Generic;

namespace ProjektAPI.Controllers
{
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
            if(model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
