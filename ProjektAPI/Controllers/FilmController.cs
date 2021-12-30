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
                    Nazwa = "Niewidzialny Człowiek",
                    Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",
                    Gatunek = "Horror",
                    CzasTrwania = "124 min",
                    OgraniczeniaWiek = Wiek.Od16lat,
                    Cena = 30
                },
                new FilmModel()
                {
                    Nazwa = "Nikt",
                    Opis = "Przechodzień jest świadkiem jak grupa mężczyzn atakuje kobietę, więc podejmuje interwencję. Tym samym staje się celem narkotykowego bossa.",
                    Gatunek = "Thriller, Akcja",
                    CzasTrwania = "92 min",
                    OgraniczeniaWiek = Wiek.Od18lat,
                    Cena = 40
                },
                new FilmModel()
                {
                    Nazwa = "Na rauszu",
                    Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",
                    Gatunek = "Dramat, Komedia",
                    CzasTrwania = "115 min",
                    OgraniczeniaWiek = Wiek.Od16lat,
                    Cena = 35
                },
                new FilmModel()
                {
                    Nazwa = "Wojna z dziadkiem",
                    Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",
                    Gatunek = "Familijny, Komedia",
                    CzasTrwania = "98 min",
                    OgraniczeniaWiek = Wiek.Od12lat,
                    Cena = 30
                },
                new FilmModel()
                {
                    Nazwa = "Nasze magiczne Encanto",
                    Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",
                    Gatunek = "Animacja, Familijny, Przygodowy",
                    CzasTrwania = "99 min",
                    OgraniczeniaWiek = Wiek.BezOgraniczen,
                    Cena = 26
                });
                _context.SaveChanges();
            }
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
