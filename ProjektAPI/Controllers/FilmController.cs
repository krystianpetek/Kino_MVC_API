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
    public class FilmController : ControllerBase
    {
        private readonly APIDatabaseContext _context;
        public FilmController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmModel>>> Get()
        {
            return await _context.Filmy.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmModel>> Get(int id)
        {
            var query = await _context.Filmy.FindAsync(id);
            if (query is null) 
                return NotFound();
            return query;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] FilmModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            _context.Filmy.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var query = await _context.Filmy.FindAsync(id);

            if (query is null)
                return NotFound();

            _context.Filmy.Remove(query);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAll(int id, [FromBody] FilmModel model)
        {
            if (id != model.Id)
                return BadRequest(); // 400

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmyExists(id))
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

        private bool FilmyExists(int id)
        {
            return _context.Filmy.Any(q => q.Id == id);
        }
    }
}
