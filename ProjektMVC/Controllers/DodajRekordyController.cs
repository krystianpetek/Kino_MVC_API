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
        private readonly HttpClient _client;
        private readonly string FilmPath;
        private readonly string KlientPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private readonly string RezerwacjaPath;
        private readonly IConfiguration _configuration;

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
                    Id = Guid.Parse("2db074c2-86cf-43ea-a9ee-8c88c50911d0"),
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
                    Id = Guid.Parse("a8317e43-ab77-4721-bf66-c53e85163e00"),
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
                    Id = Guid.Parse("258dff19-ffe3-4e0c-87e8-e2ef44b18298"),
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
            new SalaModel {Id = Guid.Parse("842ff9cf-8cf2-4719-8f85-a5f9b7f38536"), NazwaSali = "Sala 1", IloscMiejsc = 10, IloscRzedow = 5 },
            new SalaModel {Id = Guid.Parse("fe76a9b7-9778-4e5a-a9b2-38c3442a8642"), NazwaSali = "Sala 2", IloscMiejsc = 10, IloscRzedow = 10 },
            new SalaModel {Id = Guid.Parse("f6cbf553-9a95-4ee9-9093-401316cbdccd"), NazwaSali = "Sala 3", IloscMiejsc = 10, IloscRzedow = 15 },
            new SalaModel {Id = Guid.Parse("7ac5295e-9215-4745-bd70-ecfed58fd5e6"), NazwaSali = "Sala 4", IloscMiejsc = 15, IloscRzedow = 5 },
            new SalaModel {Id = Guid.Parse("ab330b4c-3577-45cc-9301-9aee0f3477bb"), NazwaSali = "Sala 5", IloscMiejsc = 15, IloscRzedow = 10 },
            new SalaModel {Id = Guid.Parse("4273708a-7ce4-4764-a44e-2f5aabb95ad2"), NazwaSali = "Sala 6", IloscMiejsc = 15, IloscRzedow = 15 },
            new SalaModel {Id = Guid.Parse("d0c06d71-47e6-45fd-8978-7ca183b43910"), NazwaSali = "Sala 7", IloscMiejsc = 20, IloscRzedow = 5 },
            new SalaModel {Id = Guid.Parse("f9586221-11ec-4bce-b84a-23dd2209c5a2"), NazwaSali = "Sala 8", IloscMiejsc = 20, IloscRzedow = 10 },
            new SalaModel {Id = Guid.Parse("209b672b-7499-45ef-b97d-494e749e9c8e"), NazwaSali = "Sala 9", IloscMiejsc = 20, IloscRzedow = 15 },
            new SalaModel {Id = Guid.Parse("0c527895-4803-4527-b07b-35ed8ecb9b0e"), NazwaSali = "Prywatna kanciapa", IloscMiejsc = 3, IloscRzedow = 3 }
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
                    Id = Guid.Parse("6fe8e345-280e-4ace-b198-7fedb778c378"),
                    Nazwa = "Nikt",
                    Opis = "Przechodzień jest świadkiem jak grupa mężczyzn atakuje kobietę, więc podejmuje interwencję. Tym samym staje się celem narkotykowego bossa.",
                    Gatunek = "Thriller, Akcja",
                    CzasTrwania = 92,
                    OgraniczeniaWiek = Wiek.Od18lat,
                    Cena = 40
                },
                new FilmModel()
                {Id = Guid.Parse("3b48356c-d900-4460-87dd-8e74bdbb2b7e"),
                Nazwa = "Niewidzialny Człowiek",
                Opis = "Po zerwaniu toksycznej relacji mąż Cecilii, naukowiec-wynalazca popełnia samobójstwo. Od tej pory zaczynają ją prześladować dziwne zdarzenia.",
                Gatunek = "Horror",
                CzasTrwania = 124,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 30
            },
                new FilmModel(){Id = Guid.Parse("b772403c-5f39-48f5-82fa-dd0cc50a670f"),
                Nazwa = "Na rauszu",
                Opis = "Czwórka nauczycieli pracujących w gimnazjum testuje alkoholową metodę, która ma polepszyć jakość ich życia.",
                Gatunek = "Dramat, Komedia",
                CzasTrwania = 115,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 35
            },
                new FilmModel(){Id = Guid.Parse("62d82401-ca06-45be-8998-939a7e4aae7d"),
                Nazwa = "Nie patrz w górę",
                Opis = "Dwójka astronomów wyrusza w tournée po mediach, aby ostrzec ludzkość przed zmierzającą w stronę Ziemi zabójczą kometą.",
                Gatunek = "Komedia",
                CzasTrwania = 145,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 35
            },
                new FilmModel(){Id = Guid.Parse("0c0a391b-a0a9-4940-91e6-c67fce295e13"),
                Nazwa = "Wojna z dziadkiem",
                Opis = "Peter zmuszony jest dzielić swoją sypialnię z Dziadkiem, który wprowadza się do jego domu po śmierci żony. Chłopak postanawia zrobić wszystko by się go pozbyć.",
                Gatunek = "Familijny, Komedia",
                CzasTrwania = 98,
                OgraniczeniaWiek = Wiek.Od12lat,
                Cena = 30
            },
                new FilmModel(){Id = Guid.Parse("a2f1f887-1eec-4ba4-86e0-6b8fe757fe0c"),
                Nazwa = "Nasze magiczne Encanto",
                Opis = "Dziewczynka pochodzi z kolumbijskiej rodziny, która obdarzona jest magicznymi mocami. Niestety ona sama nie posiada tego daru.",
                Gatunek = "Animacja, Familijny, Przygodowy",
                CzasTrwania = 99,
                OgraniczeniaWiek = Wiek.BezOgraniczen,
                Cena = 26
            },
                new FilmModel(){Id = Guid.Parse("248f9eb6-8f98-4714-90bf-b5e13a7f4031"),
                Nazwa = "Uncharted",
                Opis = "Nathan Drake wraz ze swoim przyjacielem Victorem Sullivanem i dziennikarką Eleną Fisher wyrusza na poszukiwania El Dorado.",
                Gatunek = "Przygodowy",
                CzasTrwania = 102,
                OgraniczeniaWiek = Wiek.Od7lat,
                Cena = 30
            },
                new FilmModel(){Id = Guid.Parse("9eb4dd12-a56c-4ffd-8867-617048ec7e14"),
                Nazwa = "Matrix Zmartwychwstania",
                Opis = "Podążaj za Neo, który prowadzi zwyczajne życie w San Francisco, gdzie jego terapeuta przepisuje mu niebieskie pigułki. Jednak Morfeusz oferuje mu czerwoną pigułkę i ponownie otwiera jego umysł na świat Matrix.",
                Gatunek = "Akcja, Sci-Fi",
                CzasTrwania = 148,
                OgraniczeniaWiek = Wiek.Od16lat,
                Cena = 40
            },
                new FilmModel(){Id = Guid.Parse("69a92be0-eb49-4bd7-a846-a5c22485781d"),
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

        [HttpGet("Seanse")]
        public async Task<ActionResult> EmisjeFilmow()
        {
            List<EmisjaModel> list = new List<EmisjaModel>()
            {
                new EmisjaModel()
                {
                    Id = Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"),
                    Data = DateTime.Now.AddDays(-20),
                    Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0),
                    FilmId = Guid.Parse("62D82401-CA06-45BE-8998-939A7E4AAE7D"),
                    SalaId = Guid.Parse("7AC5295E-9215-4745-BD70-ECFED58FD5E6")
                },
                new EmisjaModel()
                {
                    Id = Guid.Parse("ec0873a3-7862-44dc-9e81-99a0ba476f1d"),
                    Data = DateTime.Now.AddDays(1),
                    Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0),
                    FilmId = Guid.Parse("9EB4DD12-A56C-4FFD-8867-617048EC7E14"),
                    SalaId = Guid.Parse("F9586221-11EC-4BCE-B84A-23DD2209C5A2")
                },
                new EmisjaModel()
                {
                Id = Guid.Parse("8ac142fd-efc8-4090-b578-47d32b55694c"),
                Data = DateTime.Now.AddDays(3),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0),
                FilmId = Guid.Parse("248F9EB6-8F98-4714-90BF-B5E13A7F4031"),
                SalaId = Guid.Parse("F9586221-11EC-4BCE-B84A-23DD2209C5A2")
                },
                new EmisjaModel()
            {
                Id = Guid.Parse("4d27af37-4d51-4186-976b-0aaebdcc65dd"),
                Data = DateTime.Now.AddDays(10),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0),
                FilmId = Guid.Parse("9EB4DD12-A56C-4FFD-8867-617048EC7E14"),
                SalaId = Guid.Parse("4273708A-7CE4-4764-A44E-2F5AABB95AD2")
            },
                new EmisjaModel()
            {
                Id = Guid.Parse("355877b1-eb03-4268-98ad-19193e45ef56"),
                Data = DateTime.Now.AddDays(31),
                Godzina = DateTime.Now,
                FilmId = Guid.Parse("B772403C-5F39-48F5-82FA-DD0CC50A670F"),
                SalaId = Guid.Parse("7AC5295E-9215-4745-BD70-ECFED58FD5E6")
            },new EmisjaModel()
            {
                Id = Guid.Parse("61ffb04e-3b99-4c4b-bd28-62dc00f3c781"),
                Data = DateTime.Now.AddDays(-15),
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
                FilmId = Guid.Parse("6FE8E345-280E-4ACE-B198-7FEDB778C378"),
                SalaId = Guid.Parse("842FF9CF-8CF2-4719-8F85-A5F9B7F38536")
            },new EmisjaModel()
            {
                Id = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"),
                Data = DateTime.Now.AddDays(7),
                Godzina = DateTime.Now.AddHours(3),
                FilmId = Guid.Parse("62D82401-CA06-45BE-8998-939A7E4AAE7D"),
                SalaId = Guid.Parse("842FF9CF-8CF2-4719-8F85-A5F9B7F38536")
            },new EmisjaModel()
            {
                Id = Guid.Parse("4f1c1c4a-7388-4f0e-8b1b-f4eeeedb3136"),
                Data = DateTime.Now,
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 0, 0),
                FilmId = Guid.Parse("9EB4DD12-A56C-4FFD-8867-617048EC7E14"),
                SalaId = Guid.Parse("4273708A-7CE4-4764-A44E-2F5AABB95AD2")
            },new EmisjaModel()
            {
                Id = Guid.Parse("6f72658f-9d67-4d8f-97c5-fc59e8fdabc5"),
                Data = DateTime.Now,
                Godzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0),
                FilmId = Guid.Parse("62D82401-CA06-45BE-8998-939A7E4AAE7D"),
                SalaId = Guid.Parse("842FF9CF-8CF2-4719-8F85-A5F9B7F38536")
            }
            };
            foreach (var x in list)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(EmisjaPath, x);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                { }
            }

            return Redirect("/");
        }

        [HttpGet("RezerwacjaBiletow")]
        public async Task<ActionResult> RezerwacjaBiletow()
        {
            List<RezerwacjaModel> list = new List<RezerwacjaModel>()
            {
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 1, Miejsce = 1 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 1, Miejsce = 2 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 3, Miejsce = 3 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 10, Miejsce = 12 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 13, Miejsce = 6 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"), Rzad = 8, Miejsce = 8 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"), Rzad = 8, Miejsce = 9 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"), Rzad = 8, Miejsce = 10 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"), Rzad = 8, Miejsce = 11 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4f67bd5f-e379-4c90-af77-64bb55b514e7"), Rzad = 8, Miejsce = 12 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("6f72658f-9d67-4d8f-97c5-fc59e8fdabc5"), Rzad = 3, Miejsce = 2 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 6, Miejsce = 10 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"),Rzad = 8, Miejsce = 9 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("ec0873a3-7862-44dc-9e81-99a0ba476f1d"), Rzad = 10, Miejsce = 13 },
                new RezerwacjaModel() {Id = Guid.NewGuid(),KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("ec0873a3-7862-44dc-9e81-99a0ba476f1d"),Rzad = 10, Miejsce = 15 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4d27af37-4d51-4186-976b-0aaebdcc65dd"), Rzad = 6, Miejsce = 1 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("4d27af37-4d51-4186-976b-0aaebdcc65dd"), Rzad = 6, Miejsce = 10 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId =Guid.Parse("61ffb04e-3b99-4c4b-bd28-62dc00f3c781"), Rzad = 5, Miejsce = 10 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("61ffb04e-3b99-4c4b-bd28-62dc00f3c781"),Rzad = 10, Miejsce = 5 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("355877b1-eb03-4268-98ad-19193e45ef56"),Rzad = 10, Miejsce = 8 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("2DB074C2-86CF-43EA-A9EE-8C88C50911D0"), EmisjaId = Guid.Parse("355877b1-eb03-4268-98ad-19193e45ef56"), Rzad = 8, Miejsce = 3 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 8, Miejsce = 8 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("A8317E43-AB77-4721-BF66-C53E85163E00"), EmisjaId =Guid.Parse("1aa58006-056d-40a4-9504-27a21df23f22"), Rzad = 10, Miejsce = 7 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("ec0873a3-7862-44dc-9e81-99a0ba476f1d"), Rzad = 12, Miejsce = 11 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("A8317E43-AB77-4721-BF66-C53E85163E00"), EmisjaId =Guid.Parse("ec0873a3-7862-44dc-9e81-99a0ba476f1d"), Rzad = 5, Miejsce = 10 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("A8317E43-AB77-4721-BF66-C53E85163E00"), EmisjaId = Guid.Parse("4d27af37-4d51-4186-976b-0aaebdcc65dd"), Rzad = 4, Miejsce = 3 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("4d27af37-4d51-4186-976b-0aaebdcc65dd"), Rzad = 7, Miejsce = 5 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("61ffb04e-3b99-4c4b-bd28-62dc00f3c781"), Rzad = 4, Miejsce = 6 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("A8317E43-AB77-4721-BF66-C53E85163E00"), EmisjaId = Guid.Parse("61ffb04e-3b99-4c4b-bd28-62dc00f3c781"), Rzad = 9, Miejsce = 5 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("355877b1-eb03-4268-98ad-19193e45ef56"), Rzad = 9, Miejsce = 8 },
                new RezerwacjaModel() {Id = Guid.NewGuid(), KlientId = Guid.Parse("258DFF19-FFE3-4E0C-87E8-E2EF44B18298"), EmisjaId = Guid.Parse("355877b1-eb03-4268-98ad-19193e45ef56"), Rzad = 7, Miejsce = 3 }
            };

            foreach (var x in list)
            {
                try
                {
                    HttpResponseMessage response = await _client.PostAsJsonAsync(RezerwacjaPath, x);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                { }
            }
            return Redirect("/");
        }
    }
}