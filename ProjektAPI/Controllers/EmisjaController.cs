using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
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
            return _context.Emisja.Include(q => q.Sala).Include(q => q.Film).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<EmisjaModel>> Get(int id)
        {
            var zapytanie = _context.Emisja.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null) return NotFound();
            return Ok(zapytanie);
        }

        // POST api/<EmisjaFilmuController>
        [HttpPost]
        public ActionResult Create([FromBody] EmisjaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Emisja.Add(model);
            _context.SaveChanges();
            return Created($"emisja/{model.Id}", null);
        }

        // PUT api/<EmisjaFilmuController>/5
        [HttpPut("{id}")]
        public ActionResult EditAll(int id, [FromBody] EmisjaModel model)
        {
            var zapytanie = _context.Emisja.FirstOrDefault(q => q.Id == id);

            if (zapytanie is null)
                return NotFound();

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                zapytanie.Data = model.Data.Date;
                zapytanie.Godzina = model.Godzina;
                zapytanie.Sala = model.Sala;
                zapytanie.SalaId = model.SalaId;
                zapytanie.Film = model.Film;
                zapytanie.FilmId = model.FilmId;
            }
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<EmisjaFilmuController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zapytanie = _context.Emisja.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null)
                return NotFound();

            _context.Emisja.Remove(zapytanie);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
