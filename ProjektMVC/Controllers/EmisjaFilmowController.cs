using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class EmisjaFilmowController : Controller, IZabronDostep
    {
        private HttpClient client;
        private readonly string FilmyPath;
        private readonly string SalaPath;
        private readonly string EmisjaPath;
        private IConfiguration _configuration;
        private List<FilmModel> _filmModels;
        private List<SalaModel> _salaModels;

        public EmisjaFilmowController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            SalaPath = _configuration["ProjektAPIConfig:Url3"];
            EmisjaPath = _configuration["ProjektAPIConfig:Url5"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            ZabronDostepu();

            List<EmisjaModel> listaKlientow = null;
            HttpResponseMessage response = await client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                listaKlientow = await response.Content.ReadAsAsync<List<EmisjaModel>>();
            }
            return View(listaKlientow);
        }

        [HttpGet("Create")]
        public async Task<ActionResult> Create()
        {
            ZabronDostepu();
            await WykonajPrzypisanie();
            var emisja = new EmisjaModel() { Data = DateTime.Now.Date, Godzina = DateTime.Now };
            var krotka = new Tuple<List<FilmModel>, List<SalaModel>, EmisjaModel>(_filmModels, _salaModels, emisja);
            return View(krotka);
        }

        private async Task WykonajPrzypisanie()
        {
            HttpResponseMessage response = await client.GetAsync(SalaPath);
            if (response.IsSuccessStatusCode)
            {
                _salaModels = await response.Content.ReadAsAsync<List<SalaModel>>();
            }
            response = await client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                _filmModels = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
        }

        [HttpPost("Create")]
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

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int? id)
        {
            ZabronDostepu();
            await WykonajPrzypisanie();
            HttpResponseMessage response = await client.GetAsync(EmisjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                EmisjaModel model = await response.Content.ReadAsAsync<EmisjaModel>();
                var krotka = new Tuple<List<FilmModel>, List<SalaModel>, EmisjaModel>(_filmModels, _salaModels, model);
                return View(krotka);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Prefix = "Item3")] EmisjaModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(EmisjaPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(EmisjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                EmisjaModel model = await response.Content.ReadAsAsync<EmisjaModel>();
                await WykonajPrzypisanie();
                model.Film = _filmModels.FirstOrDefault(q => q.Id == model.FilmId);
                model.Sala = _salaModels.FirstOrDefault(q => q.Id == model.SalaId);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.DeleteAsync(EmisjaPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(EmisjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                EmisjaModel model = await response.Content.ReadAsAsync<EmisjaModel>();
                await WykonajPrzypisanie();
                model.Film = _filmModels.FirstOrDefault(q => q.Id == model.FilmId);
                model.Sala = _salaModels.FirstOrDefault(q => q.Id == model.SalaId);
                return View(model);
            }
            return NotFound();
        }
        public void ZabronDostepu()
        {
            if (User.IsInRole("Klient")) HttpContext.Response.Redirect("/");
        }
    }
}
