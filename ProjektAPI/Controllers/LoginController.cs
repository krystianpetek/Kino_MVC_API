using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ProjektAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ProjektAPI.Attributes;

namespace ProjektAPI.Controllers
{
    [Route("[controller]"), ApiController, ApiKey]
    public class LoginController : Controller
    {
        private APIDatabaseContext _context;
        public LoginController(APIDatabaseContext context)
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
        public IActionResult Index()
        {
            List<UzytkownikModel> model = _context.Login.ToList();
            if(model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
