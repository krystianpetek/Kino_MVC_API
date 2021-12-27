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

        public ListaKlientowController(IConfiguration configuration)
        {
            _configuration = configuration;
            KlienciPath = _configuration["ProjektAPIConfig:Url2"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            ZabronDostepu();
            List<KlientModel> listaKlientow = null;
            HttpResponseMessage response = await client.GetAsync(KlienciPath);
            if(response.IsSuccessStatusCode)
            {
                listaKlientow = await response.Content.ReadAsAsync<List<KlientModel>>();
            }
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
        public async Task<ActionResult> Create([Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            if(ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(KlienciPath, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit([FromRoute]int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if(response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] int id, [Bind("Id,Imie,Nazwisko,DataUrodzenia,NumerTelefonu,Email,Miasto,Ulica,KodPocztowy,Uzytkownik")] KlientModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(KlienciPath + id, model);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.DeleteAsync(KlienciPath + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }


    [HttpGet("Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(KlienciPath + id);
            if (response.IsSuccessStatusCode)
            {
                KlientModel model = await response.Content.ReadAsAsync<KlientModel>();
                return View(model);
            }
            return NotFound();
        }

        private void ZabronDostepu()
        {
            if (User.IsInRole("Klient")) HttpContext.Response.Redirect("/");
        }
    }
}
