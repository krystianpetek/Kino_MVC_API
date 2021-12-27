using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
    public class FilmController : ControllerBase
    {
        private readonly APIDatabaseContext _context;
        public FilmController(APIDatabaseContext context)
        {
            _context = context;
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            if (!_context.Filmy.Any())
            {
                _context.Filmy.AddRange(
                    new FilmModel()
                    {
                        Nazwa = "Skazani na Shawshank",
                        Opis = "Adaptacja opowiadania Stephena Kinga. Niesłusznie skazany na dożywocie bankier, stara się przetrwać w brutalnym, więziennym świecie.",
                        Gatunek = "Dramat",
                        CzasTrwania = "2 godz. 22 min.",
                        OgraniczeniaWiek = Wiek.Od16lat,
                        Cena = 35
                    },
                    new FilmModel()
                    {
                        Nazwa = "Nietykalni",
                        Opis = "Sparaliżowany milioner zatrudnia do opieki młodego chłopaka z przedmieścia, który właśnie wyszedł z więzienia.",
                        Gatunek = "Biograficzny, Dramat, Komedia",
                        CzasTrwania = "1 godz. 52 min.",
                        OgraniczeniaWiek = Wiek.Od12lat,
                        Cena = 40
                    });
            }
            _context.SaveChanges();
        }

        [HttpGet]
        public ActionResult<IEnumerable<FilmModel>> Get()
        {
            return _context.Filmy.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<FilmModel>> Get(int id)
        {
            var zapytanie = _context.Filmy.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null) return NotFound();
            return Ok(zapytanie);
        }

        [HttpPost]
        public ActionResult Create([FromBody] FilmModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Filmy.Add(model);
            _context.SaveChanges();
            return Created($"api/film/{model.Id}",null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var zapytanie = _context.Filmy.FirstOrDefault(q => q.Id == id);
            if (zapytanie is null)
                return NotFound();

            _context.Filmy.Remove(zapytanie);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult EditAll(int id, [FromBody] FilmModel model)
        {
            var zapytanie = _context.Filmy.FirstOrDefault(q => q.Id == id);
            
            if (zapytanie is null)
                return NotFound();

            if(id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                zapytanie.Nazwa = model.Nazwa;
                zapytanie.Gatunek = model.Gatunek;
                zapytanie.OgraniczeniaWiek = model.OgraniczeniaWiek;
                zapytanie.Opis = model.Opis;
                zapytanie.Cena = model.Cena;
                zapytanie.CzasTrwania = model.CzasTrwania;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
