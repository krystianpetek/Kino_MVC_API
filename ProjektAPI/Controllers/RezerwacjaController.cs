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

        // GET: api/Rezerwacja
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RezerwacjaModel>>> GetRezerwacja()
        {
            return await _context.Rezerwacja.ToListAsync();
        }

        // GET: api/Rezerwacja/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RezerwacjaModel>> GetRezerwacjaModel(int id)
        {
            var rezerwacjaModel = await _context.Rezerwacja.FindAsync(id);

            if (rezerwacjaModel == null)
            {
                return NotFound();
            }

            return rezerwacjaModel;
        }

        // PUT: api/Rezerwacja/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
            _context.Rezerwacja.Add(rezerwacjaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRezerwacjaModel", new { id = rezerwacjaModel.Id }, rezerwacjaModel);
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
    }
}
