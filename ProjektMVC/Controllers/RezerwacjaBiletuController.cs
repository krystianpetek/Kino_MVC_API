using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjektMVC.Controllers
{
    public class RezerwacjaBiletuController : Controller
    {
        private readonly HttpClient client;
        private readonly string FilmPath, KlientPath, SalaPath, EmisjaPath, RezerwacjaPath;
        private readonly IConfiguration _configuration;
        private List<KlientModel> _klientModels;
        private List<EmisjaModel> _emisjaModels;

        public RezerwacjaBiletuController(IConfiguration configuration)
        {
            //_context = context;
            _configuration = configuration;
            FilmPath = _configuration["ProjektAPIConfig:Url"];
            KlientPath = _configuration["ProjektAPIConfig:Url2"];
            SalaPath = _configuration["ProjektAPIConfig:Url3"];
            EmisjaPath = _configuration["ProjektAPIConfig:Url5"];
            RezerwacjaPath = _configuration["ProjektAPIConfig:Url6"];
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["ProjektAPIConfig:ApiKey"]);
        }
        private async Task WykonajPrzypisanie()
        {
            HttpResponseMessage response = await client.GetAsync(FilmPath);
            if (response.IsSuccessStatusCode)
            {
                _klientModels = await response.Content.ReadAsAsync<List<KlientModel>>();

            }
            response = await client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
            {
                _emisjaModels = await response.Content.ReadAsAsync<List<EmisjaModel>>();
            }
        }

        public async Task<ActionResult> Index()
        {
            await WykonajPrzypisanie();
            var zalogowanyUzytkownik = User.Identity.Name;
            List<RezerwacjaModel> rezerwacja = default;
            var response = await client.GetAsync(KlientPath);
            int idZalogowanego = default;
            if(response.IsSuccessStatusCode)
            {
                List<KlientModel> klienci = await response.Content.ReadAsAsync<List<KlientModel>>();
                idZalogowanego = klienci.FirstOrDefault(q => q.Uzytkownik.Login == zalogowanyUzytkownik).Id;
            }
            response = await client.GetAsync(RezerwacjaPath);
            if(response.IsSuccessStatusCode)
            {
                rezerwacja = await response.Content.ReadAsAsync<List<RezerwacjaModel>>();
                foreach(var item in rezerwacja)
                {
                    item.Emisja = _emisjaModels.FirstOrDefault(q => q.Id == item.EmisjaId);
                    item.Klient = _klientModels.FirstOrDefault(q => q.Id == item.KlientId);
                }
            }
            var wynik = rezerwacja.FindAll(q => q.KlientId == idZalogowanego).OrderBy(q => q.Emisja.Data);
            return View(wynik);
        }

        //[HttpGet("Create")]
        //public async Task<IActionResult> Create()
        //{

        //}
    }
}
