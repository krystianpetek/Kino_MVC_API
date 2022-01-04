using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Models;
using System;

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
        public ActionResult Klienci()
        {
            _context.Klienci.Add(new KlientModel()
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
                    Login = "krystianpetek2",
                    Haslo = "qwerty123",
                    RodzajUzytkownika = Rola.Admin
                }
            });
            _context.Klienci.Add(new KlientModel()
            {
                Imie = "Gabriel",
                Nazwisko = "Warchał",
                DataUrodzenia = new System.DateTime(1993, 03, 20),
                Miasto = "Świnna Poręba",
                Ulica = "158",
                NumerTelefonu = "777333555",
                KodPocztowy = "34-106",
                Email = "gabrielwarchal@abstrakcja.pl",
                Uzytkownik = new UzytkownikModel()
                {
                    Login = "gabrys159",
                    Haslo = "gwarchal",
                    RodzajUzytkownika = Rola.Pracownik
                }
            });
            _context.Klienci.Add(new KlientModel()
            {
                Imie = "Marcin",
                Nazwisko = "Gurdek",
                DataUrodzenia = new System.DateTime(1989, 12, 12),
                Miasto = "Koziniec",
                Ulica = "153",
                NumerTelefonu = "100200300",
                KodPocztowy = "34-106",
                Email = "marcinek@gmail.com",
                Uzytkownik = new UzytkownikModel()
                {
                    Login = "gurdekmarcin",
                    Haslo = "marcinAnon89",
                    RodzajUzytkownika = Rola.Klient
                }
            });
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("Dodawanie/SaleKinowe")]
        public ActionResult SaleKinowe()
        {
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 1", IloscMiejsc = 10, IloscRzedow = 5 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 2", IloscMiejsc = 10, IloscRzedow = 10 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 3", IloscMiejsc = 10, IloscRzedow = 15 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 4", IloscMiejsc = 15, IloscRzedow = 5 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 5", IloscMiejsc = 15, IloscRzedow = 10 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 6", IloscMiejsc = 15, IloscRzedow = 15 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 7", IloscMiejsc = 20, IloscRzedow = 5 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 8", IloscMiejsc = 20, IloscRzedow = 10 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 9", IloscMiejsc = 20, IloscRzedow = 15 });
            _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Prywatna kanciapa", IloscMiejsc = 3, IloscRzedow = 3 });
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("Dodawanie/Filmy")]
        public ActionResult Filmy()
        {
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Nikt",
                Opis = "Przechodzień jest świadkiem jak grupa mężczyzn atakuje kobietę, więc podejmuje interwencję. Tym samym staje się celem narkotykowego bossa.",
                Gatunek = "Thriller, Akcja",
                CzasTrwania = 92,
                OgraniczeniaWiek = Wiek.Od18lat,
                Cena = 40
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Niewidzialny Człowiek",
                Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",
                Gatunek = "Horror",
                CzasTrwania = 124,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Na rauszu",
                Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",
                Gatunek = "Dramat, Komedia",
                CzasTrwania = 115,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 35
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Nie patrz w górę",
                Opis = "Dwójka astronomów wyrusza w tournée po mediach, aby ostrzec ludzkość przed zmierzającą w stronę Ziemi zabójczą kometą.",
                Gatunek = "Komedia",
                CzasTrwania = 145,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 35
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Wojna z dziadkiem",
                Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",
                Gatunek = "Familijny, Komedia",
                CzasTrwania = 98,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Nasze magiczne Encanto",
                Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",
                Gatunek = "Animacja, Familijny, Przygodowy",
                CzasTrwania = 99,
                OgraniczeniaWiek = Wiek.BezOgraniczen,
                Cena = 26
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Uncharted",
                Opis = "Nathan Drake wraz ze swoim przyjacielem Victorem Sullivanem i dziennikarką Eleną Fisher wyrusza na poszukiwania El Dorado.",
                Gatunek = "Przygodowy",
                CzasTrwania = 102,
                OgraniczeniaWiek = Wiek.Od7lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Matrix Zmartwychwstania",
                Opis = "Podążaj za Neo, który prowadzi zwyczajne życie w San Francisco, gdzie jego terapeuta przepisuje mu niebieskie pigułki. Jednak Morfeusz oferuje mu czerwoną pigułkę i ponownie otwiera jego umysł na świat Matrix.",
                Gatunek = "Akcja, Sci-Fi",
                CzasTrwania = 148,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 40
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Krzyk",
                Opis = "Młoda kobieta wraca do swojego rodzinnego miasta, żeby poznać przerażające przypadki morderstw związanych z zamaskowanym seryjnym mordercą.",
                Gatunek = "Horror",
                CzasTrwania = 106,
                OgraniczeniaWiek = Wiek.Od18lat,
                Cena = 40
            });
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("Dodawanie/Seanse")]
        public ActionResult EmisjeFilmow()
        {

            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(-20),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0),
                FilmId = 5,
                SalaId = 9
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(1),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0),
                FilmId = 1,
                SalaId = 1
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(3),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                FilmId = 7,
                SalaId = 1
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(10),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0),
                FilmId = 1,
                SalaId = 2
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(31),
                Godzina = DateTime.Now,
                FilmId = 9,
                SalaId = 9
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(-15),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                FilmId = 3,
                SalaId = 8
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now.AddDays(7),
                Godzina = DateTime.Now.AddHours(3),
                FilmId = 5,
                SalaId = 8
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now,
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0),
                FilmId = 1,
                SalaId = 2
            });
            _context.Emisja.Add(new EmisjaModel()
            {
                Data = DateTime.Now,
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0),
                FilmId = 5,
                SalaId = 10
            });
            _context.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("Dodawanie/RezerwacjaBiletow")]
        public ActionResult RezerwacjaBiletow()
        {
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 1, Miejsce = 1 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 1, Miejsce = 2 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 3, Miejsce = 3 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 10, Miejsce = 12 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 13, Miejsce = 6 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 8 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 9 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 10 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 11 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 12 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 9, Rzad = 3, Miejsce = 2 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 6, Miejsce = 10 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 8, Miejsce = 9 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 2, Rzad = 10, Miejsce = 13 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 2, Rzad = 10, Miejsce = 15 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 4, Rzad = 6, Miejsce = 1 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 4, Rzad = 6, Miejsce = 10 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 6, Rzad = 5, Miejsce = 10 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 6, Rzad = 10, Miejsce = 5 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 5, Rzad = 10, Miejsce = 8 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 1, EmisjaId = 5, Rzad = 8, Miejsce = 3 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 1, Rzad = 8, Miejsce = 8 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 2, EmisjaId = 1, Rzad = 10, Miejsce = 7 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 2, Rzad = 12, Miejsce = 11 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 2, EmisjaId = 2, Rzad = 5, Miejsce = 10 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 2, EmisjaId = 4, Rzad = 4, Miejsce = 3 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 4, Rzad = 7, Miejsce = 5 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 6, Rzad = 4, Miejsce = 6 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 2, EmisjaId = 6, Rzad = 9, Miejsce = 5 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 5, Rzad = 9, Miejsce = 8 });
            _context.Rezerwacja.Add(new RezerwacjaModel() { KlientId = 3, EmisjaId = 5, Rzad = 7, Miejsce = 3 });
            _context.SaveChanges();
            return Redirect("/");

        }
    }
}
