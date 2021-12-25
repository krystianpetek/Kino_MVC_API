using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using ProjektAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjektMVC.Controllers
{
    [Authorize]
    public class ListaSalKinowychController : Controller
    {
        private readonly HttpClient client;
        private readonly string SaleKinowePath;
        private readonly IConfiguration _configuration;
        private readonly APIDatabaseContext _context;

        public ListaSalKinowychController(IConfiguration configuration, APIDatabaseContext context)
        {
            _context = context;
            _configuration = configuration;
            SaleKinowePath = _configuration["ProjektAPIConfig:Url3"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }
        public async Task<ActionResult> Index()
        {
                ZabronDostepu();

            List<SalaModel> listaSal = null;
            HttpResponseMessage response = await client.GetAsync(SaleKinowePath);
            if (response.IsSuccessStatusCode)
            {
                listaSal = await response.Content.ReadAsAsync<List<SalaModel>>();
            }
            return View(listaSal);
        }

        public IActionResult Create()
        {
            ZabronDostepu();
            return View();
        }

        public IActionResult Podglad()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            ZabronDostepu();
            if (ModelState.IsValid)
            {
                ZabronDostepu();
                HttpResponseMessage response = await client.PostAsJsonAsync(SaleKinowePath, model);
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
