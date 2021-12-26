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
    public class ListaKlientowController : Controller
    {
        private readonly HttpClient client;
        private readonly string KlienciPath;
        private readonly IConfiguration _configuration;
        private readonly APIDatabaseContext _context;

        public ListaKlientowController(IConfiguration configuration, APIDatabaseContext context)
        {
            _context = context;
            _configuration = configuration;
            KlienciPath = _configuration["ProjektAPIConfig:Url2"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index")]
        public ActionResult Index()
        {
            ZabronDostepu();
            List<KlientModel> listaOsob = _context.Klienci.Include(r => r.Uzytkownik).ToList();
            //List<KlientModel> listaOsob = ListaKlientow();
            return View(listaOsob);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            ZabronDostepu();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            _context.Klienci.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit([FromRoute] int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            List<KlientModel> listaOsob = _context.Klienci.Include(r => r.Uzytkownik).ToList();
            var osoba = listaOsob.FirstOrDefault(d => d.Id == id);
            if (osoba is null)
                return NotFound();
            return View(osoba);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            if (!ModelState.IsValid)
                return BadRequest();

            var KlientZBazy = _context.Klienci.Include(s => s.Uzytkownik).FirstOrDefault(d => d.Id == id);
            var loginZBazy = _context.Login.FirstOrDefault(d => d.Id == KlientZBazy.UzytkownikId);
            if (KlientZBazy is null)
                return NotFound();

            KlientZBazy.Imie = model.Imie;
            KlientZBazy.Nazwisko = model.Nazwisko;
            KlientZBazy.DataUrodzenia = model.DataUrodzenia;
            KlientZBazy.NumerTelefonu = model.NumerTelefonu;
            KlientZBazy.Email = model.Email;
            KlientZBazy.Miasto = model.Miasto;
            KlientZBazy.Ulica = model.Ulica;
            KlientZBazy.KodPocztowy = model.KodPocztowy;
            loginZBazy.Login = model.Uzytkownik.Login;
            loginZBazy.Haslo = model.Uzytkownik.Haslo;
            loginZBazy.RodzajUzytkownika = model.Uzytkownik.RodzajUzytkownika;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var osoba = _context.Klienci.Include(c => c.Uzytkownik).FirstOrDefault(m => m.Id == id);
            if (osoba is null)
                return NotFound();
            return View(osoba);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZabronDostepu();
            var osoba = _context.Klienci.FirstOrDefault(q => q.Id == id);
            var loginOsoba = _context.Login.FirstOrDefault(m => m.Id == osoba.UzytkownikId);
            _context.Remove(osoba);
            _context.Remove(loginOsoba);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public ActionResult Details(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var osoba = _context.Klienci.Include(d => d.Uzytkownik).FirstOrDefault(g => g.Id == id);
            if (osoba is null)
                return NotFound();
            return View(osoba);
        }

        private List<KlientModel> ListaKlientow()
        {
            List<UzytkownikModel> listaLogin = new List<UzytkownikModel>();
            foreach (var item in _context.Login)
                listaLogin.Add(item);
            List<KlientModel> listaOsob = new List<KlientModel>();
            foreach (var item in _context.Klienci)
                listaOsob.Add(item);
            foreach (var item in listaOsob)
            {
                foreach (var item2 in listaLogin)
                {
                    if (item.UzytkownikId == item2.Id)
                    {
                        item.Uzytkownik = item2;
                    }
                }
            }
            return listaOsob;
        }

        private void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
