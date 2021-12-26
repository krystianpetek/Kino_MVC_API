using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using ProjektAPI.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjektMVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ListaFilmowController : Controller
    {
        private readonly HttpClient client;
        private readonly string FilmyPath;
        private readonly IConfiguration _configuration;
        private readonly APIDatabaseContext _context;

        public ListaFilmowController(IConfiguration configuration, APIDatabaseContext context)
        {
            _context = context;
            _configuration = configuration;
            FilmyPath = _configuration["ProjektAPIConfig:Url"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index")]
        public ActionResult Index()
        {
            ZabronDostepu();
            List<FilmModel> listaFilmow = _context.Filmy.ToList();
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
        public ActionResult Create([Bind("Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            ZabronDostepu();
            _context.Filmy.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit([FromRoute] int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            List<FilmModel> listaFilmow = _context.Filmy.ToList();
            var film = listaFilmow.FirstOrDefault(d => d.Id == id);
            if (film is null)
                return NotFound();
            return View(film);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, [Bind("Nazwa, Opis, Gatunek, OgraniczeniaWiek, CzasTrwania, Cena")] FilmModel model)
        {
            ZabronDostepu();
            if (!ModelState.IsValid)
                return BadRequest();

            var film = _context.Filmy.FirstOrDefault(q => q.Id == id);
            if (film is null)
                return NotFound();

            film.Nazwa = model.Nazwa;
            film.Opis = model.Opis;
            film.Gatunek= model.Gatunek;
            film.OgraniczeniaWiek = model.OgraniczeniaWiek;
            film.CzasTrwania = model.CzasTrwania;
            film.Cena = model.Cena;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var film = _context.Filmy.FirstOrDefault(q => q.Id == id);
            if (film is null)
                return NotFound();
            return View(film);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZabronDostepu();
            var film = _context.Filmy.FirstOrDefault(q => q.Id == id);
            _context.Remove(film);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public ActionResult Details(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var film = _context.Filmy.FirstOrDefault(q => q.Id == id);
            if (film is null)
                return NotFound();
            return View(film);
        }

        public void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
