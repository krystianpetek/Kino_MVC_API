using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ProjektAPI.Models;
using System;
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
        private List<RezerwacjaModel> _rezerwacjaModels;


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
            HttpResponseMessage response = await client.GetAsync(KlientPath);
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

        [Authorize]

        [HttpGet("[controller]/Index")]
        public async Task<ActionResult> Index()
        {
            await WykonajPrzypisanie();
            var zalogowanyUzytkownik = User.Identity.Name;
            List<RezerwacjaModel> rezerwacja = default;
            var response = await client.GetAsync(KlientPath);
            int idZalogowanego = default;
            if (response.IsSuccessStatusCode)
            {
                List<KlientModel> klienci = await response.Content.ReadAsAsync<List<KlientModel>>();
                idZalogowanego = klienci.FirstOrDefault(q => q.Uzytkownik.Login == zalogowanyUzytkownik).Id;
            }
            response = await client.GetAsync(RezerwacjaPath);
            if (response.IsSuccessStatusCode)
            {
                rezerwacja = await response.Content.ReadAsAsync<List<RezerwacjaModel>>();
                foreach (var item in rezerwacja)
                {
                    item.Emisja = _emisjaModels.FirstOrDefault(q => q.Id == item.EmisjaId);
                    item.Klient = _klientModels.FirstOrDefault(q => q.Id == item.KlientId);
                }
            }
            var wynik = rezerwacja.FindAll(q => q.KlientId == idZalogowanego).OrderBy(q => q.Emisja.Data);
            return View(wynik);
        }

        [HttpGet("[controller]/Create")]
        public async Task<ActionResult> Create()
        {
            await WykonajPrzypisanie();
            var rezerwacja = new RezerwacjaModel();
            var model = new Tuple<RezerwacjaModel, List<EmisjaModel>>(rezerwacja, _emisjaModels);
            return View(model);
        }

        public async Task<ActionResult> WyborDaty(string Film)
        {
            await WykonajPrzypisanie();
            if (!string.IsNullOrWhiteSpace(Film))
            {
                foreach (var item in _emisjaModels)
                    item.Data = item.Data.Date;

                var dataWybor = _emisjaModels.Where(q => q.Film.Id == int.Parse(Film)).Select(q => q.Data).Distinct().Select(a => new SelectListItem
                {
                    Value = a.ToShortDateString(),
                    Text = a.ToShortDateString()
                }).ToList();

                return Json(dataWybor);
            }
            return null;
        }

        public async Task<ActionResult> WyborGodziny(string Data, string Film)
        {
            await WykonajPrzypisanie();
            if (!string.IsNullOrWhiteSpace(Data))
            {
                List<SelectListItem> godzinaWybor = _emisjaModels.Where(q => q.Film.Id == int.Parse(Film)).Where(q => q.Data.ToShortDateString() == Data).OrderBy(n => n.Data).Select(a => new SelectListItem
                {
                    Value = a.Godzina.ToShortTimeString(),
                    Text = a.Godzina.ToShortTimeString()
                }).ToList();
                return Json(godzinaWybor);
            }
            return null;
        }

        [HttpPost("[controller]/Create")]
        public async Task<ActionResult> Create([Bind(Prefix = "Item1")] RezerwacjaModel model)
        {
            await WykonajPrzypisanie();
            var y = _emisjaModels.Where(a => a.FilmId == model.Emisja.FilmId).Where(q => q.Data.ToShortDateString() == model.Emisja.Data.ToShortDateString());
            model.EmisjaId = y.FirstOrDefault(q => q.Data.ToShortDateString() == model.Emisja.Data.ToShortDateString()).Id;
            model.KlientId = _klientModels.FirstOrDefault(q => q.Uzytkownik.Login == User.Identity.Name).Id;

            var zwrotCreate = new Tuple<RezerwacjaModel, List<EmisjaModel>>(new RezerwacjaModel(), _emisjaModels);
            HttpResponseMessage x = await client.GetAsync(RezerwacjaPath);
            if (x.IsSuccessStatusCode)
            {
                _rezerwacjaModels = await x.Content.ReadAsAsync<List<RezerwacjaModel>>();
                foreach (var item in _rezerwacjaModels)
                {
                    if (model.Miejsce == 0 || model.Rzad == 0)
                    {
                        ViewBag.Zajete = $"Niepoprawne miejsce";
                        return View("Create", zwrotCreate);
                    }
                    if (model.Miejsce == item.Miejsce && model.Rzad == item.Rzad)
                    {
                        ViewBag.Zajete = $"To miejsce jest zajęte";
                        return View("Create", zwrotCreate);
                    }
                }
            }
            HttpResponseMessage response = await client.PostAsJsonAsync(RezerwacjaPath, model);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/Details/{id}")]
        public async Task<ActionResult> Details(int? id)
        {
            RezerwacjaModel model = default;
            List<ZajeteMiejsca> listaMiejsc = default;
            var response = await client.GetAsync(RezerwacjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<RezerwacjaModel>();
            }
            response = await client.GetAsync(RezerwacjaPath + "ZajeteMiejsca");
            if (response.IsSuccessStatusCode)
            {
                listaMiejsc = await response.Content.ReadAsAsync<List<ZajeteMiejsca>>();
                var listaMiejsc2 = listaMiejsc.Where(x => x.EmisjaId == model.EmisjaId).ToList();
                int iloscMiejsc = model.Emisja.Sala.IloscMiejsc;
                int iloscRzedow = model.Emisja.Sala.IloscRzedow;

                bool[,] tablicaBooli = new bool[iloscRzedow, iloscMiejsc];
                for (int i = 0; i < tablicaBooli.GetLength(0); i++)
                {
                    for (int j = 0; j < tablicaBooli.GetLength(1); j++)
                    {
                        foreach (var item in listaMiejsc2)
                        {
                            if (i == item.Rzad - 1 && j == item.Miejsce- 1)
                                tablicaBooli[i, j] = true;
                        }
                    }
                }
                var model2 = new Tuple<RezerwacjaModel, bool[,]>(model, tablicaBooli);
                return View(model2);
            }
            return NotFound();
        }

        [HttpGet("[controller]/CreateSiedzenie")]
        public async Task<ActionResult> CreateNaPodstawieSiedzenia(string film, int miejsce, int rzad)
        {
            await WykonajPrzypisanie();
            HttpResponseMessage response = await client.GetAsync(RezerwacjaPath);
            List<RezerwacjaModel> listarezerw = new List<RezerwacjaModel>();
            List<ZajeteMiejsca> listaMiejsc = new List<ZajeteMiejsca>();
            var rezerwacja = new RezerwacjaModel();

            if (response.IsSuccessStatusCode)
            {
                listarezerw = await response.Content.ReadAsAsync<List<RezerwacjaModel>>();
            }
            rezerwacja.Rzad = rzad;
            rezerwacja.Miejsce = miejsce;
            rezerwacja.Emisja = listarezerw.FirstOrDefault(q => q.Id == int.Parse(film)).Emisja;
            rezerwacja.EmisjaId = rezerwacja.Emisja.Id;
            response = await client.GetAsync(RezerwacjaPath + "ZajeteMiejsca");
            if (response.IsSuccessStatusCode)
            {
                listaMiejsc = await response.Content.ReadAsAsync<List<ZajeteMiejsca>>();
                var listaMiejsc2 = listaMiejsc.Where(x => x.EmisjaId == rezerwacja.EmisjaId).ToList();
                int iloscMiejsc = rezerwacja.Emisja.Sala.IloscMiejsc;
                int iloscRzedow = rezerwacja.Emisja.Sala.IloscRzedow;

                bool[,] tablicaBooli = new bool[iloscRzedow, iloscMiejsc];
                for (int i = 0; i < tablicaBooli.GetLength(0); i++)
                {
                    for (int j = 0; j < tablicaBooli.GetLength(1); j++)
                    {
                        foreach (var item in listaMiejsc2)
                        {
                            if (i == item.Rzad - 1 && j == item.Miejsce - 1)
                                tablicaBooli[i, j] = true;
                        }
                    }
                }
                var model2 = new Tuple<RezerwacjaModel, bool[,]>(rezerwacja, tablicaBooli);
                return View(model2);
            }


            return View(rezerwacja);
        }
    }
}
