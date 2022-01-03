using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;
using System;
using System.Linq;

namespace ProjektAPI.Controllers
{
    public class SeederController : ControllerBase
    {
        private readonly APIDatabaseContext _context;

        public SeederController(APIDatabaseContext context)
        {
            _context = context;
            InicjalizacjaKlienci();
            InicjalizacjaSala();
            InicjalizacjaFilm();
            InicjalizacjaEmisja();
            InicjalizacjaRezerwacja();
        }

        private void InicjalizacjaRezerwacja()
        {
            if (!_context.Rezerwacja.Any())
            {
                _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 6, Miejsce = 10 });
                _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 2, Rzad = 10, Miejsce = 15 });
                _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 4, Rzad = 6, Miejsce = 1 });
                _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 6, Rzad = 10, Miejsce = 5 });
                _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 5, Rzad = 10, Miejsce = 8 });
            _context.SaveChanges();
            }
        }

        private void InicjalizacjaKlienci()
        {
            if (!_context.Klienci.Any())
            {
                _context.Klienci.Add(new KlientModel() { Imie = "Krystian", Nazwisko = "Petek", DataUrodzenia = new System.DateTime(1998, 10, 06), Miasto = "Koziniec", Ulica = "2", NumerTelefonu = "884284782", KodPocztowy = "34-106", Email = "krystianpetek2@gmail.com", Uzytkownik = new UzytkownikModel() { Login = "krystianpetek", Haslo = "qwerty123", RodzajUzytkownika = Rola.Admin } });
                _context.Klienci.Add(new KlientModel() { Imie = "Gabriel", Nazwisko = "Warchał", DataUrodzenia = new System.DateTime(1993, 03, 20), Miasto = "Świnna Poręba", Ulica = "158", NumerTelefonu = "889410340", KodPocztowy = "34-106", Email = "warchalgabriel@gmail.com", Uzytkownik = new UzytkownikModel() { Login = "gabrys.158", Haslo = "123qweasdzxc", RodzajUzytkownika = Rola.Pracownik } });
                _context.Klienci.Add(new KlientModel() { Imie = "Sławomir", Nazwisko = "Kosarski", DataUrodzenia = new System.DateTime(1963, 05, 02), Miasto = "Wadowice", Ulica = "Ady Sari 10", NumerTelefonu = "100200300", KodPocztowy = "34-100", Email = "kosaslawek@gmail.com", Uzytkownik = new UzytkownikModel() { Login = "skosarski", Haslo = "Slavo.1963", RodzajUzytkownika = Rola.Klient } });
                _context.SaveChanges();
            }
        }

        private void InicjalizacjaSala()
        {
            if (!_context.SaleKinowe.Any())
            {
                _context.SaleKinowe.Add(new SalaModel(){NazwaSali = "Sala 1", IloscMiejsc = 15, IloscRzedow = 6 });
                _context.SaleKinowe.Add(new SalaModel(){NazwaSali = "Sala 2",IloscMiejsc = 12,IloscRzedow = 8});
                _context.SaleKinowe.Add(new SalaModel(){NazwaSali = "PRYWATNA",IloscMiejsc = 10,IloscRzedow = 10});
                _context.SaveChanges();
            }
        }

        private void InicjalizacjaFilm()
        {
            if (!_context.Filmy.Any())
            {
                _context.Filmy.Add(new FilmModel(){Nazwa = "Niewidzialny Człowiek",Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",Gatunek = "Horror",CzasTrwania = "124 min",OgraniczeniaWiek = Wiek.Od16lat,Cena = 30});                
                _context.Filmy.Add(new FilmModel(){Nazwa = "Nikt",Opis = "Przechodzień jest świadkiem jak grupa mężczyzn atakuje kobietę, więc podejmuje interwencję. Tym samym staje się celem narkotykowego bossa.",Gatunek = "Thriller, Akcja",CzasTrwania = "92 min",OgraniczeniaWiek = Wiek.Od18lat,Cena = 40});
                _context.Filmy.Add(new FilmModel(){Nazwa = "Na rauszu",Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",Gatunek = "Dramat, Komedia",CzasTrwania = "115 min",OgraniczeniaWiek = Wiek.Od16lat,Cena = 35});
                _context.Filmy.Add(new FilmModel(){Nazwa = "Wojna z dziadkiem",Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",Gatunek = "Familijny, Komedia",CzasTrwania = "98 min",OgraniczeniaWiek = Wiek.Od12lat,Cena = 30});
                _context.Filmy.Add(new FilmModel(){Nazwa = "Nasze magiczne Encanto",Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",Gatunek = "Animacja, Familijny, Przygodowy",CzasTrwania = "99 min",OgraniczeniaWiek = Wiek.BezOgraniczen,Cena = 26});
                _context.SaveChanges();
            }
        }

        private void InicjalizacjaEmisja()
        {
            if (!_context.Emisja.Any())
            {
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now.AddHours(10),Godzina = DateTime.Now.AddHours(10),FilmId = 2,SalaId = 1});
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now,Godzina = DateTime.Now,FilmId = 1,SalaId = 1});
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now.AddDays(2),Godzina = DateTime.Now.AddHours(10),FilmId = 4,SalaId = 2});
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now.AddDays(-15),Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),FilmId = 2,SalaId = 1});
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now.AddHours(3),Godzina = DateTime.Now.AddHours(3),FilmId = 5,SalaId = 2});
                _context.Emisja.Add(new EmisjaModel(){Data = DateTime.Now.AddHours(3),Godzina = DateTime.Now.AddHours(3),FilmId = 1,SalaId = 1});
                _context.SaveChanges();
            }
        }
    }
}
