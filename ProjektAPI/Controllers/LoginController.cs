using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektAPI.Database;
namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class LoginController : Controller
    {
        private APIDatabaseContext _context;

        public LoginController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<UzytkownikModel>> Index()
        {
            List<UzytkownikModel> model = await _context.Login.ToListAsync();
            if (model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}