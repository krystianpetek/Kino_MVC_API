using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
    public class KlientController : ControllerBase
    {
        private readonly APIDatabaseContext _context;
        public KlientController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<KlientModel>> Get()
        {
            return _context.Klienci.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<KlientModel>> Get(int id)
        {
            var zapytanie = _context.Klienci.FirstOrDefault(q => q.Id == id);
            if(zapytanie is null) return NotFound();
            return Ok(zapytanie);
        }

        [HttpPost]
        public ActionResult Create([FromBody] KlientModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Klienci.Add(model);
            _context.SaveChanges();
            return Created($"film/{model.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zapytanie = _context.Klienci.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null)
                return NotFound();

            _context.Klienci.Remove(zapytanie);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult EditAll(int id, [FromBody] KlientModel model)
        {
            var zapytanie = _context.Klienci.FirstOrDefault(q => q.Id == id);

            if (zapytanie is null)
                return NotFound();

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                zapytanie.Imie = model.Imie;
                zapytanie.Nazwisko = model.Nazwisko;
                zapytanie.DataUrodzenia = model.DataUrodzenia;
                zapytanie.NumerTelefonu = model.NumerTelefonu;
                zapytanie.Miasto = model.Miasto;
                zapytanie.Ulica = model.Ulica;
                zapytanie.KodPocztowy = model.KodPocztowy;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
