using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EmisjaController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public EmisjaController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmisjaModel>>> Get()
        {
            return await _context.Emisja.Include(q => q.Sala).Include(w => w.Film).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmisjaModel>> Get(int id)
        {
            var query = await _context.Emisja.Include(q => q.Sala).Include(w=>w.Film).FirstOrDefaultAsync(e=>e.Id == id);
            if (query is null)
                return NotFound();
            return query;
        }

        [HttpPost]
        public async Task<ActionResult<EmisjaModel>> Create([FromBody] EmisjaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            _context.Emisja.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get",new {id = model.Id}, model); // zwraca w response, obiekt który był requestowany postem
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = await _context.Emisja.FindAsync(id);

            if (query is null)
                return NotFound();

            _context.Emisja.Remove(query);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAll(int id, [FromBody] EmisjaModel model)
        {
            if (id != model.Id)
                return BadRequest(); // 400

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!EmisjaExists(id))
                {
                    return NotFound(); // 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204
        }

        private bool EmisjaExists(int id)
        {
            return _context.Emisja.Any(q => q.Id == id);
        }
    }
}
