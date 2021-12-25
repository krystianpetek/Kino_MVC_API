using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using ProjektAPI.Models;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{


    public class ListaFilmowController : Controller
    {
        private readonly HttpClient client;
        private readonly string FilmyPath;
        private readonly IConfiguration _configuration;

        public ListaFilmowController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }
        public async Task<ActionResult> Index()
        {
            List<FilmModel> listaOsob = null;
            HttpResponseMessage response = await client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                listaOsob = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
            return View(listaOsob);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nazwa,Opis,Gatunek,OgraniczeniaWiek,CzasTrwania,Cena")] FilmModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(FilmyPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
