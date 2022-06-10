using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers.DodawanieRekordow
{
    [Route("Dodawanie")]
    public class DodajRekordyController : Controller
    {
        private HttpClient _client;
        private readonly string FilmPath;
        private readonly string KlientPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private readonly string RezerwacjaPath;
        private IConfiguration _configuration;

        public DodajRekordyController(IConfiguration configuration)
        {
            _configuration = configuration;
            KlientPath = _configuration["ProjektAPIConfig:Klient"];
            SalaPath = _configuration["ProjektAPIConfig:Sala"];
            FilmPath = _configuration["ProjektAPIConfig:Film"];
            EmisjaPath = _configuration["ProjektAPIConfig:Emisja"];
            RezerwacjaPath = _configuration["ProjektAPIConfig:Rezerwacja"];
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Klienci")]
        public async Task<ActionResult> Klienci()
        {
            List<KlientModel> list = new List<KlientModel>()
            {
                new KlientModel
                {
                    Imie = "Krystian" ,
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
                },
                new KlientModel
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
                },
                new KlientModel
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
                }
            };

            foreach (var x in list)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(KlientPath, x);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                { }
            }
            return Redirect("/");
        }

        [HttpGet("SaleKinowe")]
        public async Task<ActionResult> SaleKinowe()
        {
            List<SalaModel> list = new List<SalaModel>()
        {
            new SalaModel { NazwaSali = "Sala 1", IloscMiejsc = 10, IloscRzedow = 5 },
            new SalaModel { NazwaSali = "Sala 2", IloscMiejsc = 10, IloscRzedow = 10 },
            new SalaModel() { NazwaSali = "Sala 3", IloscMiejsc = 10, IloscRzedow = 15 },
            new SalaModel() { NazwaSali = "Sala 4", IloscMiejsc = 15, IloscRzedow = 5 },
            new SalaModel() { NazwaSali = "Sala 5", IloscMiejsc = 15, IloscRzedow = 10 },
            new SalaModel() { NazwaSali = "Sala 6", IloscMiejsc = 15, IloscRzedow = 15 },
            new SalaModel() { NazwaSali = "Sala 7", IloscMiejsc = 20, IloscRzedow = 5 },
            new SalaModel() { NazwaSali = "Sala 8", IloscMiejsc = 20, IloscRzedow = 10 },
            new SalaModel() { NazwaSali = "Sala 9", IloscMiejsc = 20, IloscRzedow = 15 },
            new SalaModel() { NazwaSali = "Prywatna kanciapa", IloscMiejsc = 3, IloscRzedow = 3 }
        };

            foreach (var x in list)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(SalaPath, x);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                { }
            }

            return Redirect("/");
        }

        [HttpGet("Filmy")]
        public async Task<ActionResult> Filmy()
        {
            List<FilmModel> list = new List<FilmModel>()
            {
                new FilmModel()
                {
                    Nazwa = "Nikt",
                    Opis = "Przechodzień jest świadkiem jak grupa mężczyzn atakuje kobietę, więc podejmuje interwencję. Tym samym staje się celem narkotykowego bossa.",
                    Gatunek = "Thriller, Akcja",
                    CzasTrwania = 92,
                    OgraniczeniaWiek = Wiek.Od18lat,
                    Cena = 40
                },
                new FilmModel()
                {
                Nazwa = "Niewidzialny Człowiek",
                Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",
                Gatunek = "Horror",
                CzasTrwania = 124,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 30
            },
                new FilmModel(){
                Nazwa = "Na rauszu",
                Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",
                Gatunek = "Dramat, Komedia",
                CzasTrwania = 115,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 35
            },
                new FilmModel(){
                Nazwa = "Nie patrz w górę",
                Opis = "Dwójka astronomów wyrusza w tournée po mediach, aby ostrzec ludzkość przed zmierzającą w stronę Ziemi zabójczą kometą.",
                Gatunek = "Komedia",
                CzasTrwania = 145,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 35
            },
                new FilmModel(){
                Nazwa = "Wojna z dziadkiem",
                Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",
                Gatunek = "Familijny, Komedia",
                CzasTrwania = 98,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 30
            },
                new FilmModel(){
                Nazwa = "Nasze magiczne Encanto",
                Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",
                Gatunek = "Animacja, Familijny, Przygodowy",
                CzasTrwania = 99,
                OgraniczeniaWiek = Wiek.BezOgraniczen,
                Cena = 26
            },
                new FilmModel(){
                Nazwa = "Uncharted",
                Opis = "Nathan Drake wraz ze swoim przyjacielem Victorem Sullivanem i dziennikarką Eleną Fisher wyrusza na poszukiwania El Dorado.",
                Gatunek = "Przygodowy",
                CzasTrwania = 102,
                OgraniczeniaWiek = Wiek.Od7lat,
                Cena = 30
            },
                new FilmModel(){
                Nazwa = "Matrix Zmartwychwstania",
                Opis = "Podążaj za Neo, który prowadzi zwyczajne życie w San Francisco, gdzie jego terapeuta przepisuje mu niebieskie pigułki. Jednak Morfeusz oferuje mu czerwoną pigułkę i ponownie otwiera jego umysł na świat Matrix.",
                Gatunek = "Akcja, Sci-Fi",
                CzasTrwania = 148,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 40
            },
                new FilmModel(){
                Nazwa = "Krzyk",
                Opis = "Młoda kobieta wraca do swojego rodzinnego miasta, żeby poznać przerażające przypadki morderstw związanych z zamaskowanym seryjnym mordercą.",
                Gatunek = "Horror",
                CzasTrwania = 106,
                OgraniczeniaWiek = Wiek.Od18lat,
                Cena = 40
            }
            };
            foreach (var x in list)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(FilmPath, x);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                { }
            }
            return Redirect("/");
        }

        //[HttpGet("Seanse")]
        //public async Task<ActionResult> EmisjeFilmow()
        //{
        //    List<EmisjaModel> list = new List<EmisjaModel>()
        //    {
        //        new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(-20),
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0),
        //        FilmId = 5,
        //        SalaId = 9
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(1),
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0),
        //        FilmId = 1,
        //        SalaId = 1
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(3),
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
        //        FilmId = 7,
        //        SalaId = 1
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(10),
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0),
        //        FilmId = 1,
        //        SalaId = 2
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(31),
        //        Godzina = DateTime.Now,
        //        FilmId = 9,
        //        SalaId = 9
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(-15),
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
        //        FilmId = 3,
        //        SalaId = 8
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now.AddDays(7),
        //        Godzina = DateTime.Now.AddHours(3),
        //        FilmId = 5,
        //        SalaId = 8
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now,
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0),
        //        FilmId = 1,
        //        SalaId = 2
        //    },new EmisjaModel()
        //    {
        //        Data = DateTime.Now,
        //        Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0),
        //        FilmId = 5,
        //        SalaId = 10
        //    }
        //    };
        //    foreach (var x in list)
        //    {
        //        try
        //        {
        //            HttpResponseMessage response = await _client.PostAsJsonAsync(EmisjaPath, x);
        //            response.EnsureSuccessStatusCode();
        //        }
        //        catch (HttpRequestException)
        //        { }
        //    }

        //    return Redirect("/");
        //}

        //[HttpGet("RezerwacjaBiletow")]
        //public async Task<ActionResult> RezerwacjaBiletow()
        //{
        //    List<RezerwacjaModel> list = new List<RezerwacjaModel>()
        //    {
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 1, Miejsce = 1 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 1, Miejsce = 2 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 3, Miejsce = 3 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 10, Miejsce = 12 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 13, Miejsce = 6 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 8 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 9 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 10 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 11 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 7, Rzad = 8, Miejsce = 12 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 9, Rzad = 3, Miejsce = 2 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 6, Miejsce = 10 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 1, Rzad = 8, Miejsce = 9 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 2, Rzad = 10, Miejsce = 13 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 2, Rzad = 10, Miejsce = 15 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 4, Rzad = 6, Miejsce = 1 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 4, Rzad = 6, Miejsce = 10 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 6, Rzad = 5, Miejsce = 10 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 6, Rzad = 10, Miejsce = 5 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 5, Rzad = 10, Miejsce = 8 },
        //        new RezerwacjaModel() { KlientId = 1, EmisjaId = 5, Rzad = 8, Miejsce = 3 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 1, Rzad = 8, Miejsce = 8 },
        //        new RezerwacjaModel() { KlientId = 2, EmisjaId = 1, Rzad = 10, Miejsce = 7 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 2, Rzad = 12, Miejsce = 11 },
        //        new RezerwacjaModel() { KlientId = 2, EmisjaId = 2, Rzad = 5, Miejsce = 10 },
        //        new RezerwacjaModel() { KlientId = 2, EmisjaId = 4, Rzad = 4, Miejsce = 3 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 4, Rzad = 7, Miejsce = 5 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 6, Rzad = 4, Miejsce = 6 },
        //        new RezerwacjaModel() { KlientId = 2, EmisjaId = 6, Rzad = 9, Miejsce = 5 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 5, Rzad = 9, Miejsce = 8 },
        //        new RezerwacjaModel() { KlientId = 3, EmisjaId = 5, Rzad = 7, Miejsce = 3 }
        //    };

        //    foreach (var x in list)
        //    {
        //        try
        //        {
        //            HttpResponseMessage response = await _client.PostAsJsonAsync(RezerwacjaPath, x);
        //            response.EnsureSuccessStatusCode();
        //        }
        //        catch (HttpRequestException)
        //        { }
        //    }
        //    return Redirect("/");
        //}
    }
}