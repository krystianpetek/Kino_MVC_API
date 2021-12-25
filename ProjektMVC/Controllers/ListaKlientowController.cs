using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using ProjektAPI.Models;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Authorize]

    public class ListaKlientowController : Controller
    {
        private readonly HttpClient client;
        private readonly string KlienciPath;
        private readonly IConfiguration _configuration;

        public ListaKlientowController(IConfiguration configuration)
        {
            _configuration = configuration;
            KlienciPath = _configuration["ProjektAPIConfig:Url2"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }
        public async Task<ActionResult> Index()
        {
            List<KlientModel> listaOsob = null;
            HttpResponseMessage response = await client.GetAsync(KlienciPath);
            if (response.IsSuccessStatusCode)
            {
                listaOsob = await response.Content.ReadAsAsync<List<KlientModel>>();
            }
            return View(listaOsob);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(KlienciPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
