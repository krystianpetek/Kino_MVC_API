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
    public class ListaSalKinowychController : Controller, IZabronDostep
    {

        private readonly HttpClient client;
        private readonly string SaleKinowePath;
        private readonly IConfiguration _configuration;

        public ListaSalKinowychController(IConfiguration configuration)
        {

            _configuration = configuration;
            SaleKinowePath = _configuration["ProjektAPIConfig:Url3"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }

        [HttpGet("Index"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Index()
        {
            ZabronDostepu();
            List<SalaModel> listaSal = null;
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath);
            if (odpowiedz.IsSuccessStatusCode)
            {
                listaSal = await odpowiedz.Content.ReadAsAsync<List<SalaModel>>();
            }
            return View(listaSal);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            ZabronDostepu();
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            ZabronDostepu();
            HttpResponseMessage odpowiedz = await client.PostAsJsonAsync(SaleKinowePath, model);
            odpowiedz.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit([FromRoute] int? id)
        {
            ZabronDostepu();
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath + id);
            if (odpowiedz.IsSuccessStatusCode)
            {
                SalaModel model = await odpowiedz.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute] int id, [Bind("Id, NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                HttpResponseMessage odpowiedz = await client.PutAsJsonAsync(SaleKinowePath + id, model);
                odpowiedz.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("Delete")]
        public async Task<ActionResult> Delete(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath + id);
            if (odpowiedz.IsSuccessStatusCode)
            {
                SalaModel model = await odpowiedz.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZabronDostepu();
            HttpResponseMessage odpowiedz = await client.DeleteAsync(SaleKinowePath + id);
            odpowiedz.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            ZabronDostepu();
            HttpResponseMessage response = await client.GetAsync(SaleKinowePath + id);
            if (response.IsSuccessStatusCode)
            {
                SalaModel model = await response.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }

        public void ZabronDostepu()
        {
            if (User.IsInRole("Klient")) HttpContext.Response.Redirect("/");
        }
    }
}
