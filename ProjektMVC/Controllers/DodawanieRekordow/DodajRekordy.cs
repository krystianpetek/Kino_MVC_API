using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Models;

namespace ProjektMVC.Controllers.DodawanieRekordow
{
    public class DodajRekordy : Controller
    {
        private readonly APIDatabaseContext _context;
        public DodajRekordy(APIDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("Dodawanie/Klienci")]
        public ViewResult Klienci()
        {
            _context.Klienci.Add(new KlientModel() { Imie = "Krystian", Nazwisko = "Petek", DataUrodzenia = new System.DateTime(1998, 10, 06), Miasto = "Koziniec", Ulica = "2", NumerTelefonu = "884284782", KodPocztowy = "34-106", Email = "krystianpetek2@gmail.com", Uzytkownik = new UzytkownikModel() { Login = "krystianpetek2", Haslo = "qwerty123", RodzajUzytkownika = Rola.Admin } });
            _context.Klienci.Add(new KlientModel() { Imie = "Gabriel", Nazwisko = "Warchał", DataUrodzenia = new System.DateTime(1993, 03, 20), Miasto = "Świnna Poręba", Ulica = "158", NumerTelefonu = "777333555", KodPocztowy = "34-106", Email = "gabrielwarchal@abstrakcja.pl", Uzytkownik = new UzytkownikModel() { Login = "gabrys159", Haslo = "gwarchal", RodzajUzytkownika = Rola.Pracownik } });
            _context.Klienci.Add(new KlientModel() { Imie = "Marcin", Nazwisko = "Gurdek", DataUrodzenia = new System.DateTime(1989, 12, 12), Miasto = "Koziniec", Ulica = "153", NumerTelefonu = "100200300", KodPocztowy = "34-106", Email = "marcinek@gmail.com", Uzytkownik = new UzytkownikModel() { Login = "gurdekmarcin", Haslo = "marcinAnon89", RodzajUzytkownika = Rola.Klient } });
            _context.SaveChanges();
            return View("/", "Dodano");
        }
        //public IActionResult SaleKinowe()
        //{
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 1", IloscMiejsc = 10, IloscRzedow = 6 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 2", IloscMiejsc = 12, IloscRzedow = 8 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 3", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 4", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 5", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 6", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 7", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 8", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala Prywatna", IloscMiejsc = 3, IloscRzedow = 5 });
        //    _context.SaveChanges();
        //}
    }
}
