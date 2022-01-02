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
                CzasTrwania = "92 min",
                OgraniczeniaWiek = Wiek.Od18lat,
                Cena = 40
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Niewidzialny Człowiek",
                Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",
                Gatunek = "Horror",
                CzasTrwania = "124 min",
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Na rauszu",
                Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",
                Gatunek = "Dramat, Komedia",
                CzasTrwania = "115 min",
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 35
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Nie patrz w górę",
                Opis = "Dwójka astronomów wyrusza w tournée po mediach, aby ostrzec ludzkość przed zmierzającą w stronę Ziemi zabójczą kometą.",
                Gatunek = "Komedia",
                CzasTrwania = "145 min",
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 35
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Wojna z dziadkiem",
                Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",
                Gatunek = "Familijny, Komedia",
                CzasTrwania = "98 min",
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Nasze magiczne Encanto",
                Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",
                Gatunek = "Animacja, Familijny, Przygodowy",
                CzasTrwania = "99 min",
                OgraniczeniaWiek = Wiek.BezOgraniczen,
                Cena = 26
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Uncharted",
                Opis = "Nathan Drake wraz ze swoim przyjacielem Victorem Sullivanem i dziennikarką Eleną Fisher wyrusza na poszukiwania El Dorado.",
                Gatunek = "Przygodowy",
                CzasTrwania = "102 min",
                OgraniczeniaWiek = Wiek.Od7lat,
                Cena = 30
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Matrix Zmartwychwstania",
                Opis = "Podążaj za Neo, który prowadzi zwyczajne życie w San Francisco, gdzie jego terapeuta przepisuje mu niebieskie pigułki. Jednak Morfeusz oferuje mu czerwoną pigułkę i ponownie otwiera jego umysł na świat Matrix.",
                Gatunek = "Akcja, Sci-Fi",
                CzasTrwania = "148 min",
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 40
            });
            _context.Filmy.Add(new FilmModel()
            {
                Nazwa = "Krzyk",
                Opis = "Młoda kobieta wraca do swojego rodzinnego miasta, żeby poznać przerażające przypadki morderstw związanych z zamaskowanym seryjnym mordercą.",
                Gatunek = "Horror",
                CzasTrwania = "106 min",
                OgraniczeniaWiek = Wiek.Od18lat,
                Cena = 40
            });
            _context.SaveChanges();
            return Redirect("/");
        }


    }
}
