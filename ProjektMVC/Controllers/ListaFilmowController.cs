using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ListaFilmowController : Controller
    {
        private readonly HttpClient client;
        private readonly string FilmyPath;
        private readonly IConfiguration _configuration;

        public ListaFilmowController(IConfiguration configuration)
        {
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Film"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Index()
        {
            List<FilmModel> listaFilmow = null;
            HttpResponseMessage response = await client.GetAsync(FilmyPath);
            if (response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
            return View(listaFilmow);
        }

        [HttpGet("Create"), Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create"), Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(FilmyPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Edit/{id}"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Edit(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if (response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}"), Authorize(Roles = "Admin, Pracownik"), ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*Guid id*/int id, [Bind("Id,Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(FilmyPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if (response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}"), Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(FilmyPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}"), Authorize(Roles = "Admin, Pracownik")]
        public async Task<ActionResult> Details(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if (response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }
    }
}