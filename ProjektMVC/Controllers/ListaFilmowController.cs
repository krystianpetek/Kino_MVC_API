using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using ProjektAPI.Models;

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
            //_context = context;
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            ZabronDostepu();
            List<FilmModel> listaFilmow = null;
            HttpResponseMessage response = await client.GetAsync(FilmyPath);//_context.Filmy.ToList();
            if(response.IsSuccessStatusCode)
            {
                listaFilmow = await response.Content.ReadAsAsync<List<FilmModel>>();
            }
            return View(listaFilmow);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            ZabronDostepu();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            ZabronDostepu();
            if(ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(FilmyPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if(response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(FilmyPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if(response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.DeleteAsync(FilmyPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(FilmyPath + id);
            if(response.IsSuccessStatusCode)
            {
                FilmModel model = await response.Content.ReadAsAsync<FilmModel>();
                return View(model);
            }
            return NotFound();
        }

        public void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
