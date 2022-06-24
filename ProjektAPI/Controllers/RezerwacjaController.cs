using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Database;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezerwacjaController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public RezerwacjaController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RezerwacjaModel>>> Get()
        {
            return await _context.Rezerwacja.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RezerwacjaModel>> Get(Guid id/*int id*/)
        {
            var query = await _context.Rezerwacja.FindAsync(id);
            if (query == null)
                return NotFound();
            return query;
        }

        [HttpPost]
        public async Task<ActionResult<RezerwacjaModel>> Create(RezerwacjaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = await _context.Rezerwacja.FirstOrDefaultAsync(q => q.EmisjaId == model.EmisjaId);

            if (query != null)
            {
                if (query.Miejsce == model.Miejsce && query.Rzad == model.Rzad)
                    return BadRequest();
            }
            _context.Rezerwacja.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id/*int id*/)
        {
            var query = await _context.Rezerwacja.FindAsync(id);

            if (query is null)
                return NotFound();

            _context.Rezerwacja.Remove(query);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id/*int id*/, RezerwacjaModel rezerwacjaModel)
        {
            if (id != rezerwacjaModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(rezerwacjaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezerwacjaModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool RezerwacjaModelExists(Guid id/*int id*/)
        {
            return _context.Rezerwacja.Any(e => e.Id == id);
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