using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
    public class SalaController : ControllerBase
    {
        private readonly APIDatabaseContext _context;
        public SalaController(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SalaModel>> Get()
        {
            return _context.SaleKinowe.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SalaModel>> Get(int id)
        {
            var zapytanie = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null) return NotFound();
            return Ok(zapytanie);
        }

        [HttpPost]
        public ActionResult Create([FromBody] SalaModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.SaleKinowe.Add(model);
            _context.SaveChanges();
            return Created($"film/{model.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zapytanie = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null)
                return NotFound();

            _context.SaleKinowe.Remove(zapytanie);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult EditAll(int id, [FromBody] SalaModel model)
        {
            var zapytanie = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);

            if (zapytanie is null)
                return NotFound();

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                zapytanie.NazwaSali = model.NazwaSali;
                zapytanie.IloscMiejsc = model.IloscMiejsc;
                zapytanie.IloscRzedow = model.IloscRzedow;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}