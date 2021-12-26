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
            List<KlientModel> listaKlientow = _context.Klienci.Include(q => q.Uzytkownik).ToList();
            //List<KlientModel> listaKlientow = ListaKlientow();
            return View(listaKlientow);
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
            List<KlientModel> listaKlientow = _context.Klienci.Include(q => q.Uzytkownik).ToList();
            var klient = listaKlientow.FirstOrDefault(q => q.Id == id);
            if (klient is null)
                return NotFound();
            return View(klient);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            if (!ModelState.IsValid)
                return BadRequest();

            var klientZBazy= _context.Klienci.Include(q => q.Uzytkownik).FirstOrDefault(q => q.Id == id);
            var loginZBazy = _context.Login.FirstOrDefault(q => q.Id == klientZBazy.UzytkownikId);
            if (klientZBazy is null)
                return NotFound();

            klientZBazy.Imie = model.Imie;
            klientZBazy.Nazwisko = model.Nazwisko;
            klientZBazy.DataUrodzenia = model.DataUrodzenia;
            klientZBazy.NumerTelefonu = model.NumerTelefonu;
            klientZBazy.Email = model.Email;
            klientZBazy.Miasto = model.Miasto;
            klientZBazy.Ulica = model.Ulica;
            klientZBazy.KodPocztowy = model.KodPocztowy;
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
            var klient = _context.Klienci.Include(q => q.Uzytkownik).FirstOrDefault(q => q.Id == id);
            if (klient is null)
                return NotFound();
            return View(klient);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZabronDostepu();
            var klient = _context.Klienci.FirstOrDefault(q => q.Id == id);
            var uzytkownik = _context.Login.FirstOrDefault(q => q.Id == klient.UzytkownikId);
            _context.Remove(klient);
            _context.Remove(uzytkownik);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public ActionResult Details(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var klient = _context.Klienci.Include(q => q.Uzytkownik).FirstOrDefault(q => q.Id == id);
            if (klient is null)
                return NotFound();
            return View(klient);
        }

        private List<KlientModel> ListaKlientow()
        {
            List<UzytkownikModel> listaUzytkownikow = new List<UzytkownikModel>();
            foreach (var item in _context.Login)
                listaUzytkownikow.Add(item);
            List<KlientModel> listaKlientow = new List<KlientModel>();
            foreach (var item in _context.Klienci)
                listaKlientow.Add(item);
            foreach (var item in listaKlientow)
            {
                foreach (var item2 in listaUzytkownikow)
                {
                    if (item.UzytkownikId == item2.Id)
                    {
                        item.Uzytkownik = item2;
                    }
                }
            }
            return listaKlientow;
        }

        private void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
