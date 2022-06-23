using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class EmisjaFilmowController : Controller
    {
        private readonly HttpClient client;
        private readonly string FilmyPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private readonly IConfiguration _configuration;
        private List<FilmModel> _filmModels;
        private List<SalaModel> _salaModels;

        public EmisjaFilmowController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Film"];
            SalaPath = _configuration["ProjektAPIConfig:Sala"];
            EmisjaPath = _configuration["ProjektAPIConfig:Emisja"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        private async Task SalaAsync()
        {
            HttpResponseMessage response = await client.GetAsync(SalaPath);
            if (response.IsSuccessStatusCode)
            {
                _salaModels = await response.Content.ReadAsAsync<List<SalaModel>>();
            }
        }

        private async Task FilmyAsync()
        {
            HttpResponseMessage response = await client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                _filmModels = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
        }

        [HttpGet("Index"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Index()
        {
            List<EmisjaModel> listaKlientow = null;
            HttpResponseMessage response = await client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                listaKlientow = await response.Content.ReadAsAsync<List<EmisjaModel>>();
            }
            return View(listaKlientow.OrderByDescending(d => d.Data));
        }

        [HttpGet("Create"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            await FilmyAsync();
            await SalaAsync();
            var emisja = new EmisjaModel() { Data = DateTime.Now.Date, Godzina = DateTime.Now };
            var krotka = new Tuple<List<FilmModel>, List<SalaModel>, EmisjaModel>(_filmModels, _salaModels, emisja);
            return View(krotka);
        }

        [HttpPost("Create"), Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Prefix = "Item3")] EmisjaModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(EmisjaPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Edit/{id}"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Edit(/*Guid id*/int id)
        {
            await SalaAsync();
            await FilmyAsync();
            HttpResponseMessage response = await client.GetAsync(EmisjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                EmisjaModel model = await response.Content.ReadAsAsync<EmisjaModel>();
                var krotka = new Tuple<List<FilmModel>, List<SalaModel>, EmisjaModel>(_filmModels, _salaModels, model);
                return View(krotka);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}"), Authorize(Roles = "Admin, Pracownik")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*Guid id*/int id, [Bind(Prefix = "Item3")] EmisjaModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(EmisjaPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Delete(/*Guid id*/int id)
        {
            return await AddItToConfirmation(id);
        }

        [HttpPost("Delete/{id}"), Authorize(Roles = "Admin, Pracownik")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(EmisjaPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Details(/*Guid id*/int id)
        {
            return await AddItToConfirmation(id);
        }

        private async Task<ActionResult> AddItToConfirmation(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(EmisjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                EmisjaModel model = await response.Content.ReadAsAsync<EmisjaModel>();
                await FilmyAsync();
                await SalaAsync();
                model.Film = _filmModels.FirstOrDefault(q => q.Id == model.FilmId);
                model.Sala = _salaModels.FirstOrDefault(q => q.Id == model.SalaId);
                return View(model);
            }
            return NotFound();
        }
    }
}