using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using ProjektMVC.Models;
using System;
using ProjektMVC;

namespace ProjektAPI.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient _client;
        private readonly string FilmyPath;
        private readonly string KlientPath;
        private readonly string EmisjaPath;
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            KlientPath = _configuration["ProjektAPIConfig:Url2"];
            EmisjaPath = _configuration["ProjektAPIConfig:Url5"];
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        public async Task<IActionResult> Index()
        {
            bool odpowiedz = false;
            List<UzytkownikModel> listaUzytkownikow=new List<UzytkownikModel>();
            HttpResponseMessage response = await _client.GetAsync(KlientPath);
            if(response.IsSuccessStatusCode)
            {

                List<KlientModel> listaKlientow = await response.Content.ReadAsAsync<List<KlientModel>>();
                if(listaKlientow.Count > 0)
                {
                    foreach(var uzytkownik in listaKlientow)
                    {
                        listaUzytkownikow.Add(new UzytkownikModel()
                        {
                            Id = uzytkownik.Uzytkownik.Id,
                            Login = uzytkownik.Uzytkownik.Login,
                            Haslo = uzytkownik.Uzytkownik.Haslo,
                            RodzajUzytkownika = uzytkownik.Uzytkownik.RodzajUzytkownika
                        });
                    }
                    odpowiedz = true;
                }
            }
            return View(new Tuple<bool,List<UzytkownikModel>>(odpowiedz,listaUzytkownikow));
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
        public async Task<IActionResult> AktualnieEmitowaneFilmy(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)

        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

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
                        posortowanaLista  = aktualnaListaFilmow.OrderByDescending(x => x.Data).ToList();
                    }
                }
            }

            int pageSize = 10;
            return View(PaginatedList<AktualnieEmitowaneFilmy>.Create(posortowanaLista, pageNumber ?? 1, pageSize));
        }
    }
}
