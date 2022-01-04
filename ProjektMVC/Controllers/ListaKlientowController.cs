﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Create"), Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(KlienciPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Edit/{id}"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Edit([FromRoute] int? id)
        {
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}"), Authorize(Roles = "Admin,Pracownik"), ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(KlienciPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(KlienciPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Details(int? id)
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
