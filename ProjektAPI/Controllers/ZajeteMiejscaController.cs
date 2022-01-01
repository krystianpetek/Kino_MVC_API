using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektAPI.Controllers
{
    public class ZajeteMiejscaController : Controller
    {
        private readonly APIDatabaseContext _context;

        public ZajeteMiejscaController(APIDatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("ZajeteMiejsca")]
        public async Task<ActionResult> ZajeteMiejsca()
        {
            var pobierz = await _context.Rezerwacja.ToListAsync();
            List<ZajeteMiejsca> model = new List<ZajeteMiejsca>();
            foreach (var item in pobierz)
            {
                var modelik = new ZajeteMiejsca()
                {
                    Id = item.Id,
                    EmisjaId = item.EmisjaId,
                    Miejsce = item.Miejsce,
                    Rzad = item.Rzad
                };
                model.Add(modelik);
            }
            return Ok(model);
        }
    }
}
