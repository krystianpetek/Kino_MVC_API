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
    public class ListaKlientowController : Controller
    {
        private readonly HttpClient client;
        private readonly string UzytkownikPath;
        private readonly string KlienciPath;
        private readonly IConfiguration _configuration;
        private List<KlientModel> _listaKlientow;
        private List<UzytkownikModel> _listaUzytkowniokow;

        public ListaKlientowController(IConfiguration configuration)
        {
            _configuration = configuration;
            UzytkownikPath = _configuration["ProjektAPIConfig:Login"];
            KlienciPath = _configuration["ProjektAPIConfig:Klient"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        private async Task PobierzKlientow()
        {
            HttpResponseMessage response = await client.GetAsync(KlienciPath);
            if (response.IsSuccessStatusCode)
                _listaKlientow = await response.Content.ReadAsAsync<List<KlientModel>>();
        }

        private async Task PobierzKlientowUzytkownikow()
        {
            HttpResponseMessage response = await client.GetAsync(UzytkownikPath);
            if (response.IsSuccessStatusCode)
                _listaUzytkowniokow = await response.Content.ReadAsAsync<List<UzytkownikModel>>();
        }

        [HttpGet("Index"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Index()
        {
            List<KlientModel> listaKlientow = null;
            HttpResponseMessage response = await client.GetAsync(KlienciPath);
            if (response.IsSuccessStatusCode)
            {
                listaKlientow = await response.Content.ReadAsAsync<List<KlientModel>>();
            }
            return View(listaKlientow);
        }

        [HttpGet("Create"), Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KlientModel model)
        {
            if (ModelState.IsValid)
            {
                await PobierzKlientow();
                if (_listaKlientow.Select(x => x.Email).Contains(model.Email))
                {
                    TempData["duplikat"] = "Login lub email są już zajęte";
                    return Redirect($"Create");
                }
                if (_listaKlientow.Select(x => x.Uzytkownik.Login).Contains(model.Uzytkownik.Login))
                {
                    TempData["duplikat"] = "Login lub email są już zajęte";
                    return Redirect($"Create");
                }
                HttpResponseMessage response = await client.PostAsJsonAsync(KlienciPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["duplikat"] = "Błędne dane";
            }
            return RedirectToAction($"Create");
        }

        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([FromRoute] /*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] /*Guid id*/int id, KlientModel model)
        {
            await PobierzKlientowUzytkownikow();
            model.Uzytkownik = _listaUzytkowniokow.SingleOrDefault(q => q.Login == model.Uzytkownik.Login);
            model.UzytkownikId = model.Uzytkownik.Id;
            if (ModelState.IsValid) // todo - duplikaty
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(KlienciPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}"), Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(KlienciPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Details(/*Guid id*/int id)
        {
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }
    }
}