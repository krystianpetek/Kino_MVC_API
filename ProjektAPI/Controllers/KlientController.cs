using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Database;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class KlientController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public KlientController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KlientModel>>> Get()
        {
            return await _context.Klienci.Include(q => q.Uzytkownik).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KlientModel>> Get(/*Guid id*/int id)
        {
            var query = await _context.Klienci.Include(q => q.Uzytkownik).FirstAsync(w => w.Id == id);
            if (query is null)
                return NotFound();
            return query;
        }

        [HttpPost]
        public async Task<ActionResult<KlientModel>> Create([FromBody] KlientModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var query = _context.Klienci;
            if (query.Any(q => q.Email == model.Email) || query.Any(w => w.Uzytkownik.Login == model.Uzytkownik.Login))
                return BadRequest();

            _context.Klienci.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(/*Guid id*/int id)
        {
            var queryKlient = await _context.Klienci.FindAsync(id);
            if (queryKlient is null)
                return NotFound();

            var queryUzytkownik = await _context.Login.FindAsync(queryKlient.UzytkownikId);
            if (queryUzytkownik is null)
                return NotFound();

            _context.Klienci.Remove(queryKlient);
            _context.Login.Remove(queryUzytkownik);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAll(/*Guid id*/int id, [FromBody] KlientModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (model.UzytkownikId != model.Uzytkownik.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlientExists(id))
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

        private bool KlientExists(/*Guid id*/int id)
        {
            return _context.Klienci.Any(q => q.Id == id);
        }
    }
}