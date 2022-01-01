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
        private List<FilmModel> _filmModels;
        private List<KlientModel> _klientModels;
        private List<EmisjaModel> _emisjaModels;
        private List<RezerwacjaModel> _rezerwacjaModels;
        private List<ZajeteMiejsca> _zajeteMiejsca;

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
        private async Task FilmAsync()
        {
            HttpResponseMessage response = await client.GetAsync(FilmPath);
            if (response.IsSuccessStatusCode)
                _filmModels = await response.Content.ReadAsAsync<List<FilmModel>>();
        }
        private async Task KlienciAsync()
        {
            HttpResponseMessage response = await client.GetAsync(KlientPath);
            if (response.IsSuccessStatusCode)
                _klientModels = await response.Content.ReadAsAsync<List<KlientModel>>();
        }
        private async Task EmisjaAsync()
        {
            HttpResponseMessage response = await client.GetAsync(EmisjaPath);
            if (response.IsSuccessStatusCode)
                _emisjaModels = await response.Content.ReadAsAsync<List<EmisjaModel>>();
        }
        private async Task RezerwacjaAsync()
        {
            HttpResponseMessage response = await client.GetAsync(RezerwacjaPath);
            if (response.IsSuccessStatusCode)
                _rezerwacjaModels = await response.Content.ReadAsAsync<List<RezerwacjaModel>>();
        }
        private async Task ZajeteMiejscaAsync()
        {
            HttpResponseMessage response = await client.GetAsync(RezerwacjaPath + "ZajeteMiejsca");
            if (response.IsSuccessStatusCode)
                _zajeteMiejsca = await response.Content.ReadAsAsync<List<ZajeteMiejsca>>();
        }
        private bool[,] ZajeteSiedzenia(RezerwacjaModel model)
        {
            var listaMiejsc = _zajeteMiejsca.Where(x => x.EmisjaId == model.EmisjaId).ToList();
            int iloscMiejsc = model.Emisja.Sala.IloscMiejsc;
            int iloscRzedow = model.Emisja.Sala.IloscRzedow;

            bool[,] siedzenia = new bool[iloscRzedow, iloscMiejsc];
            for (int i = 0; i < siedzenia.GetLength(0); i++)
            {
                for (int j = 0; j < siedzenia.GetLength(1); j++)
                {
                    foreach (var item in listaMiejsc)
                    {
                        if (i == item.Rzad - 1 && j == item.Miejsce - 1)
                            siedzenia[i, j] = true;
                    }
                }
            }

            return siedzenia;
        }

        [Authorize]
        [HttpGet("[controller]/Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString, int? pageNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            await KlienciAsync();
            await RezerwacjaAsync();
            await EmisjaAsync();

            var zalogowanyUzytkownik = User.Identity.Name;
            int idZalogowanego = _klientModels.FirstOrDefault(q => q.Uzytkownik.Login == zalogowanyUzytkownik).Id;

            foreach (var item in _rezerwacjaModels)
            {
                item.Emisja = _emisjaModels.FirstOrDefault(q => q.Id == item.EmisjaId);
                item.Klient = _klientModels.FirstOrDefault(q => q.Id == item.KlientId);
            }

            var wynik = _rezerwacjaModels.FindAll(q => q.KlientId == idZalogowanego).OrderByDescending(q => q.Emisja.Data).ToList();

            int pageSize = 10;
            return View(PaginatedList<RezerwacjaModel>.Create(wynik, pageNumber ?? 1, pageSize));
        }

        [HttpGet("[controller]/Create")]
        public async Task<ActionResult> Create()
        {
            await EmisjaAsync();
            var model = new Tuple<RezerwacjaModel, List<EmisjaModel>>(new RezerwacjaModel(), _emisjaModels);
            return View(model);
        }

        public async Task<ActionResult> WyborDaty(string Film)
        {
            await EmisjaAsync();
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
            await EmisjaAsync();
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
            await EmisjaAsync();
            await RezerwacjaAsync();
            await KlienciAsync();
            model.Miejsce += 1;
            model.Rzad += 1;
            model.Emisja = _rezerwacjaModels.FirstOrDefault(q => q.Id == model.Id).Emisja;
            model.EmisjaId = _rezerwacjaModels.FirstOrDefault(q => q.Id == model.Id).EmisjaId;
            model.KlientId = _klientModels.FirstOrDefault(q => q.Uzytkownik.Login == User.Identity.Name).Id;

            var przefiltowana = _rezerwacjaModels.Where(q => q.EmisjaId == model.EmisjaId);
            var ModelWyjsciowy = new Tuple<RezerwacjaModel, List<EmisjaModel>>(new RezerwacjaModel(), _emisjaModels);
            foreach (var item in przefiltowana)
            {
                if (model.Miejsce <= 0 || model.Rzad <= 0 || model.Miejsce > model.Emisja.Sala.IloscMiejsc || model.Rzad > model.Emisja.Sala.IloscRzedow)
                {
                    ViewBag.Zajete = $"Niepoprawne miejsce";
                    return View("Create", ModelWyjsciowy);
                }
                if (model.Miejsce == item.Miejsce && model.Rzad == item.Rzad)
                {
                    ViewBag.Zajete = $"To miejsce jest zajęte";
                    return View("Create", ModelWyjsciowy);
                }
            }

            HttpResponseMessage response = await client.PostAsJsonAsync(RezerwacjaPath, model);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            await ZajeteMiejscaAsync();
            RezerwacjaModel model = new RezerwacjaModel();
            var response = await client.GetAsync(RezerwacjaPath + id);
            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<RezerwacjaModel>();
            }

            bool[,] siedzenia = ZajeteSiedzenia(model);

            var model2 = new Tuple<RezerwacjaModel, bool[,]>(model, siedzenia);
            return View(model2);
        }


        [HttpGet("[controller]/CreateSiedzenie")]
        public async Task<ActionResult> CreateNaPodstawieSiedzenia(string film, int miejsce, int rzad)
        {
            await RezerwacjaAsync();
            await ZajeteMiejscaAsync();
            RezerwacjaModel model = new RezerwacjaModel();

            model.Id = int.Parse(film);
            model.Rzad = rzad - 1;
            model.Miejsce = miejsce - 1;
            model.Emisja = _rezerwacjaModels.FirstOrDefault(q => q.Id == int.Parse(film)).Emisja;
            model.EmisjaId = model.Emisja.Id;

            bool[,] siedzenia = ZajeteSiedzenia(model);
            var model2 = new Tuple<RezerwacjaModel, bool[,]>(model, siedzenia);
            return View(model2);
        }
    }
}
