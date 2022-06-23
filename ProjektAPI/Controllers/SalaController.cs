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
    public class SalaController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public SalaController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaModel>>> Get()
        {
            return await _context.SaleKinowe.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaModel>> Get(/*Guid id*/int id)
        {
            var query = await _context.SaleKinowe.FindAsync(id);
            if (query is null)
                return NotFound();
            return query;
        }

        [HttpPost]
        public async Task<ActionResult<SalaModel>> Create([FromBody] SalaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            if (_context.SaleKinowe.Any(q => q.NazwaSali == model.NazwaSali))
                return BadRequest();

            _context.SaleKinowe.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(/*Guid id*/int id)
        {
            var query = await _context.SaleKinowe.FindAsync(id);

            if (query is null)
                return NotFound();

            _context.SaleKinowe.Remove(query);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAll(/*Guid id*/int id, [FromBody] SalaModel model)
        {
            if (id != model.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
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

        private bool SalaExists(/*Guid id*/int id)
        {
            return _context.SaleKinowe.Any(q => q.Id == id);
        }
    }
}