using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using ProjektAPI.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ProjektMVC.Controllers
{
    [Authorize]

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
        public async Task<ActionResult> Index()
        {
            ZabronDostepu();
            List<KlientModel> listaOsob = null;

            HttpResponseMessage response = await client.GetAsync(KlienciPath);
            if (response.IsSuccessStatusCode)
            {
                List<UzytkownikModel> listaLogin = new List<UzytkownikModel>();
                foreach (var item in _context.Login)
                    listaLogin.Add(item);

                listaOsob = await response.Content.ReadAsAsync<List<KlientModel>>();

                foreach (var item in listaOsob)
                {
                    foreach (var item2 in listaLogin)
                        if (item.UzytkownikId == item2.Id)
                        {
                            item.Uzytkownik = item2;
                        }
                }
            }
            return View(listaOsob);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(KlienciPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
