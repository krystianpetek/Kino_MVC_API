using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Route("[controller]")]
    public class EmisjaFilmowController : Controller
    {
        private HttpClient _client;
        private readonly string FilmyPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private IConfiguration _configuration;

        public EmisjaFilmowController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            SalaPath = _configuration["ProjektAPIConfig:Url3"];
            EmisjaPath = _configuration["ProjektAPIConfig:Url5"];
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Create")]
        public async Task<ActionResult> Create()
        {
            List<FilmModel> listaFilmow = null;
            List<SalaModel> listaSal = null;

            HttpResponseMessage response = await _client.GetAsync(SalaPath);
            if (response.IsSuccessStatusCode)
            {
                listaSal = await response.Content.ReadAsAsync<List<SalaModel>>();
            }
            response = await _client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
            var krotka = new Tuple<List<FilmModel>, List<SalaModel>,EmisjaModel>(listaFilmow, listaSal,null);
            return View(krotka);
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Prefix = "Item3")] EmisjaModel model)
        {
            var zapytanieFilm = await _client.GetAsync(FilmyPath+model.FilmId);
            var film = await zapytanieFilm.Content.ReadAsAsync<FilmModel>();
            model.Film = film;
            
            var zapytanieSala = await _client.GetAsync(SalaPath+model.SalaId);
            var sala = await zapytanieSala.Content.ReadAsAsync<SalaModel>();
            model.Sala = sala;

                HttpResponseMessage response = await _client.PostAsJsonAsync(EmisjaPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("/");//nameof(Index));

        }
    }
}
