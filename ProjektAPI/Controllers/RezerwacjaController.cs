using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;

namespace ProjektAPI.Controllers
{
    [Route("[controller]")]
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
            return await _context.Rezerwacja.Include(q=>q.Emisja).Include(q=>q.Klient).Include(q=>q.Klient.Uzytkownik).Include(q=>q.Emisja.Sala).Include(q=>q.Emisja.Film).ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<RezerwacjaModel> Get(int id)
        {
            var model = _context.Rezerwacja.Include(q => q.Emisja).Include(q => q.Emisja.Sala).Include(q => q.Emisja.Film).Include(q => q.Klient).Include(q => q.Klient.Uzytkownik).FirstOrDefault(q => q.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezerwacjaModel(int id, RezerwacjaModel rezerwacjaModel)
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

        // POST: api/Rezerwacja
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RezerwacjaModel>> PostRezerwacjaModel(RezerwacjaModel rezerwacjaModel)
        {
            rezerwacjaModel.Emisja = null;
            _context.Rezerwacja.Add(rezerwacjaModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Rezerwacja/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRezerwacjaModel(int id)
        {
            var rezerwacjaModel = await _context.Rezerwacja.FindAsync(id);
            if (rezerwacjaModel == null)
            {
                return NotFound();
            }

            _context.Rezerwacja.Remove(rezerwacjaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RezerwacjaModelExists(int id)
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

