using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using ProjektMVC.Models;
using System;

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
                return View(aktualnaListaFilmow);
            }
            return BadRequest();

        }
        
        public async Task<ActionResult> AktualnieEmitowaneFilmy()
        {
            List<EmisjaModel> listaFilmow = null;
            HttpResponseMessage response = await _client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<EmisjaModel>>();

                var aktualnaListaFilmow = new List<AktualnieEmitowaneFilmy>();
                foreach (var item in listaFilmow)
                {
                    aktualnaListaFilmow.Add(new AktualnieEmitowaneFilmy()
                    {
                        Id = item.Id,
                        Data = item.Data,
                        NazwaFilmu = item.Film.Nazwa
                    });
                }
                return View(aktualnaListaFilmow);
            }
            return BadRequest();

        }
    }
}
