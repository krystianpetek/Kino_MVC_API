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
        private readonly string EmisjaPath;
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            EmisjaPath = _configuration["ProjektAPIConfig:Url5"];
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        public IActionResult Index()
        {
            return View();
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
