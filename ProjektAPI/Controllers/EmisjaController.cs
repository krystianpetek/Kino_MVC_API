using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAPI.Controllers
{
    [Route("api/[controller]"), ApiController, ApiKey]
    public class EmisjaController : ControllerBase
    {
        private readonly APIDatabaseContext _context;
        public EmisjaController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmisjaModel>> Get()
        {
            return _context.EmisjaFilmu.Include(q=>q.Sala).Include(q=>q.Film).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<EmisjaModel>> Get(int id)
        {
            var zapytanie = _context.EmisjaFilmu.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null) return NotFound();
            return Ok(zapytanie);
        }

        // POST api/<EmisjaFilmuController>
        [HttpPost]
        public ActionResult Create([FromBody] EmisjaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.EmisjaFilmu.Add(model);
            _context.SaveChanges();
            return Created($"api/emisja/{model.Id}", null);
        }

        // PUT api/<EmisjaFilmuController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmisjaFilmuController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zapytanie = _context.EmisjaFilmu.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null)
                return NotFound();

            _context.EmisjaFilmu.Remove(zapytanie);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
