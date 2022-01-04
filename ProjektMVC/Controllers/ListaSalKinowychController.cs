using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    [Authorize, Route("[controller]")]
    public class ListaSalKinowychController : Controller
    {
        private readonly HttpClient client;
        private readonly string SaleKinowePath;
        private readonly IConfiguration _configuration;
        private List<SalaModel> _listaSalKinowych;

        public ListaSalKinowychController(IConfiguration configuration)
        {
            _configuration = configuration;
            SaleKinowePath = _configuration["ProjektAPIConfig:Url3"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }
        private async Task PobierzSaleKinowe()
        {
            HttpResponseMessage response = await client.GetAsync(SaleKinowePath);
            if (response.IsSuccessStatusCode)
                _listaSalKinowych = await response.Content.ReadAsAsync<List<SalaModel>>();
        }

        [HttpGet("Index"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Index()
        {
            List<SalaModel> listaSal = null;
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath);
            if (odpowiedz.IsSuccessStatusCode)
            {
                listaSal = await odpowiedz.Content.ReadAsAsync<List<SalaModel>>();
            }
            return View(listaSal);
        }

        [HttpGet("Create"), Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create"), Authorize(Roles = "Admin"), ValidateAntiForgeryToken] // modelstate
        public async Task<ActionResult> Create([Bind("NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            await PobierzSaleKinowe();
            if (!_listaSalKinowych.Select(x => x.NazwaSali).Contains(model.NazwaSali))
            {
                HttpResponseMessage odpowiedz = await client.PostAsJsonAsync(SaleKinowePath, model);
                odpowiedz.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            TempData["duplikat"] = "Istnieje już sala o takiej nazwie";
            return RedirectToAction("Create");
        }

        [HttpGet("Edit/{id}"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Edit([FromRoute] int? id)
        {
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath + id);
            if (odpowiedz.IsSuccessStatusCode)
            {
                SalaModel model = await odpowiedz.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}"), Authorize(Roles = "Admin,Pracownik"), ValidateAntiForgeryToken]  // modelstate
        public async Task<ActionResult> Edit([FromRoute] int id, [Bind("Id, NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            await PobierzSaleKinowe();
            if (!_listaSalKinowych.Select(x => x.NazwaSali).Contains(model.NazwaSali))
            {
                HttpResponseMessage odpowiedz = await client.PutAsJsonAsync(SaleKinowePath + id, model);
                odpowiedz.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            TempData["duplikat"] = "Istnieje już sala o takiej nazwie";
            return Redirect($"{model.Id}");
        }

        [HttpGet("Delete"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            HttpResponseMessage odpowiedz = await client.GetAsync(SaleKinowePath + id);
            if (odpowiedz.IsSuccessStatusCode)
            {
                SalaModel model = await odpowiedz.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }

        [HttpPost("Delete/{id}"), Authorize(Roles = "Admin"), ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage odpowiedz = await client.DeleteAsync(SaleKinowePath + id);
            odpowiedz.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}"), Authorize(Roles = "Admin,Pracownik")]
        public async Task<ActionResult> Details(int? id)
        {
            HttpResponseMessage response = await client.GetAsync(SaleKinowePath + id);
            if (response.IsSuccessStatusCode)
            {
                SalaModel model = await response.Content.ReadAsAsync<SalaModel>();
                return View(model);
            }
            return NotFound();
        }
    }
}
