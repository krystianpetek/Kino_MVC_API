using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using ProjektAPI.Producer;
using ProjektMVC;
using ProjektMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageProducer _producer;
        private HttpClient _client;
        private readonly string FilmyPath;
        private readonly string KlientPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private readonly string RezerwacjaPath;
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration, IMessageProducer producer)
        {
            _configuration = configuration;
            _producer = producer;
            KlientPath = _configuration["ProjektAPIConfig:Klient"];
            SalaPath = _configuration["ProjektAPIConfig:Sala"];
            FilmyPath = _configuration["ProjektAPIConfig:Film"];
            EmisjaPath = _configuration["ProjektAPIConfig:Emisja"];
            RezerwacjaPath = _configuration["ProjektAPIConfig:Rezerwacja"];
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        public async Task<IActionResult> Index()
        {
            List<bool> list = new List<bool>() { false, false, false, false, false };
            list[0] = false; // uzytkownik
            List<UzytkownikModel> listaUzytkownikow = new List<UzytkownikModel>();
            HttpResponseMessage response = await _client.GetAsync(KlientPath);
            if (response.IsSuccessStatusCode)
            {
                List<KlientModel> listaKlientow = await response.Content.ReadAsAsync<List<KlientModel>>();
                if (listaKlientow.Count > 0)
                {
                    foreach (var uzytkownik in listaKlientow)
                    {
                        listaUzytkownikow.Add(new UzytkownikModel()
                        {
                            Id = uzytkownik.Uzytkownik.Id,
                            Login = uzytkownik.Uzytkownik.Login,
                            Haslo = uzytkownik.Uzytkownik.Haslo,
                            RodzajUzytkownika = uzytkownik.Uzytkownik.RodzajUzytkownika
                        });
                    }
                }
                if (listaKlientow.Count >= 3)
                    list[0] = true;
            }

            list[1] = false; // sala kinowa
            response = await _client.GetAsync(SalaPath);
            if (response.IsSuccessStatusCode)
            {
                List<SalaModel> listaSal = await response.Content.ReadAsAsync<List<SalaModel>>();
                if (listaSal.Count > 0)
                    list[1] = true;
            }

            list[2] = false; // film
            response = await _client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                List<FilmModel> listaFilmow = await response.Content.ReadAsAsync<List<FilmModel>>();
                if (listaFilmow.Count > 0)
                    list[2] = true;
            }

            list[3] = false; // seans
            response = await _client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                List<EmisjaModel> listaSeansow = await response.Content.ReadAsAsync<List<EmisjaModel>>();
                if (listaSeansow.Count > 0)
                    list[3] = true;
            }

            list[4] = false; // bilety
            response = await _client.GetAsync(RezerwacjaPath);
            if (response.IsSuccessStatusCode)
            {
                List<RezerwacjaModel> listaRezerwacji = await response.Content.ReadAsAsync<List<RezerwacjaModel>>();
                if (listaRezerwacji.Count > 0)
                    list[4] = true;
            }

            var message = string.Empty;

            return View(new Tuple<List<bool>, List<UzytkownikModel>, string>(list, listaUzytkownikow, message));
        }

        [HttpPost]
        public async Task<IActionResult> Message(string Item3)
        {
            _producer.SendMessage(Item3);
            return LocalRedirect("/");
        }

        public async Task<ActionResult> AktualneFilmy()
        {
            List<FilmModel> listaFilmow = null;
            HttpResponseMessage response = await _client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<FilmModel>>();

                var aktualnaListaFilmow = new List<AktualneFilmyModel>();
                if (listaFilmow.Count > 0)
                {
                    foreach (var item in listaFilmow)
                    {
                        aktualnaListaFilmow.Add(new AktualneFilmyModel()
                        {
                            Nazwa = item.Nazwa,
                            Opis = item.Opis,
                            Gatunek = item.Gatunek,
                            Wiek = item.OgraniczeniaWiek.ToString(),
                            Cena = item.Cena
                        });
                    }
                }
                return View(aktualnaListaFilmow);
            }
            return BadRequest();
        }

        public async Task<IActionResult> AktualnieEmitowaneFilmy(int? pageNumber)
        {
            List<AktualnieEmitowaneFilmy> posortowanaLista = new List<AktualnieEmitowaneFilmy>();
            List<EmisjaModel> listaFilmow = null;
            HttpResponseMessage response = await _client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<EmisjaModel>>();
                var aktualnaListaFilmow = new List<AktualnieEmitowaneFilmy>();
                if (listaFilmow.Count > 0)
                {
                    foreach (var item in listaFilmow)
                    {
                        aktualnaListaFilmow.Add(new AktualnieEmitowaneFilmy()
                        {
                            Id = item.Id,
                            Data = item.Data,
                            Godzina = item.Godzina,
                            NazwaFilmu = item.Film.Nazwa
                        });
                        posortowanaLista = aktualnaListaFilmow.OrderByDescending(x => x.Data).ToList();
                    }
                }
            }

            int pageSize = 10;
            return View(PaginatedList<AktualnieEmitowaneFilmy>.Create(posortowanaLista, pageNumber ?? 1, pageSize));
        }
    }
}