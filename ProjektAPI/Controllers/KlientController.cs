using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Attributes;
using ProjektAPI.Models;
using System;
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
            Inicjalizacja();
        }

        private void Inicjalizacja()
        {
            if (!_context.Klienci.Any())
            {
                _context.Klienci.AddRange(
                new KlientModel()
                {
                    Imie = "Krystian",
                    Nazwisko = "Petek",
                    DataUrodzenia = new System.DateTime(1998, 10, 06),
                    Miasto = "Koziniec",
                    Ulica = "2",
                    NumerTelefonu = "884284782",
                    KodPocztowy = "34-106",
                    Email = "krystianpetek2@gmail.com",
                    Uzytkownik = new UzytkownikModel()
                    {
                        Login = "krystianpetek",
                        Haslo = "qwerty123",
                        RodzajUzytkownika = Rola.Admin
                    }
                },
                new KlientModel()
                {
                    Imie = "Gabriel",
                    Nazwisko = "Warchał",
                    DataUrodzenia = new System.DateTime(1993, 03, 20),
                    Miasto = "Świnna Poręba",
                    Ulica = "158",
                    NumerTelefonu = "889410340",
                    KodPocztowy = "34-106",
                    Email = "warchalgabriel@gmail.com",
                    Uzytkownik = new UzytkownikModel()
                    {
                        Login = "gabrys.158",
                        Haslo = "123qweasdzxc",
                        RodzajUzytkownika = Rola.Pracownik
                    }
                },
                new KlientModel()
                {
                    Imie = "Sławomir",
                    Nazwisko = "Kosarski",
                    DataUrodzenia = new System.DateTime(1963, 05, 02),
                    Miasto = "Wadowice",
                    Ulica = "Ady Sari 10",
                    NumerTelefonu = "100200300",
                    KodPocztowy = "34-100",
                    Email = "kosaslawek@gmail.com",
                    Uzytkownik = new UzytkownikModel()
                    {
                        Login = "skosarski",
                        Haslo = "Slavo.1963",
                        RodzajUzytkownika = Rola.Klient
                    }
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<KlientModel>> Get()
        {
            List<KlientModel> listaKlientow = ListaKlientow();
            return Ok(listaKlientow);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<KlientModel>> Get(int id)
        {
            var zapytanie = _context.Klienci.Include(q=>q.Uzytkownik).FirstOrDefault(q => q.Id == id);
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
            var zapytanieKlient = _context.Klienci.FirstOrDefault(q => q.Id == id);
            if (zapytanieKlient is null)
                return NotFound();

            var zapytanieUzytkownik = _context.Login.FirstOrDefault(q=>q.Id == zapytanieKlient.UzytkownikId);
            if (zapytanieKlient is null)
                return NotFound();

            _context.Klienci.Remove(zapytanieKlient);
            _context.Login.Remove(zapytanieUzytkownik);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult EditAll(int id, [FromBody] KlientModel model)
        {
            var klientZBazy = _context.Klienci.Include(q => q.Uzytkownik).FirstOrDefault(q => q.Id == id);
            var loginZBazy = _context.Login.FirstOrDefault(q => q.Id == klientZBazy.UzytkownikId);

            if (klientZBazy is null)
                return NotFound();

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                klientZBazy.Imie = model.Imie;
                klientZBazy.Nazwisko = model.Nazwisko;
                klientZBazy.DataUrodzenia = model.DataUrodzenia;
                klientZBazy.NumerTelefonu = model.NumerTelefonu;
                klientZBazy.Email = model.Email;
                klientZBazy.Miasto = model.Miasto;
                klientZBazy.Ulica = model.Ulica;
                klientZBazy.KodPocztowy = model.KodPocztowy;
                loginZBazy.Login = model.Uzytkownik.Login;
                loginZBazy.Haslo = model.Uzytkownik.Haslo;
                loginZBazy.RodzajUzytkownika = model.Uzytkownik.RodzajUzytkownika;
            }
            _context.SaveChanges();
            return Ok();
        }
        private List<KlientModel> ListaKlientow()
        {
            List<UzytkownikModel> listaUzytkownikow = new List<UzytkownikModel>();
            foreach (var item in _context.Login)
                listaUzytkownikow.Add(item);
            List<KlientModel> listaKlientow = new List<KlientModel>();
            foreach (var item in _context.Klienci)
                listaKlientow.Add(item);
            foreach (var item in listaKlientow)
            {
                foreach (var item2 in listaUzytkownikow)
                {
                    if (item.UzytkownikId == item2.Id)
                    {
                        item.Uzytkownik = item2;
                    }
                }
            }
            return listaKlientow;
        }

    }

}
