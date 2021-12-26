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

        [HttpGet("Index")]
        public ActionResult Index()
        {
            ZabronDostepu();
            List<SalaModel> listaSal = _context.SaleKinowe.ToList();
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
        public ActionResult Create([Bind("NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            ZabronDostepu();
            _context.SaleKinowe.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public ActionResult Edit([FromRoute] int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            List<SalaModel> listaSal = _context.SaleKinowe.ToList();
            var sala = listaSal.FirstOrDefault(d => d.Id == id);
            if (sala is null)
                return NotFound();
            return View(sala);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, [Bind("NazwaSali, IloscRzedow, IloscMiejsc")] SalaModel model)
        {
            ZabronDostepu();
            if (!ModelState.IsValid)
                return BadRequest();

            var sala = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            if (sala is null)
                return NotFound();

            sala.NazwaSali = model.NazwaSali;
            sala.IloscMiejsc= model.IloscMiejsc;
            sala.IloscRzedow = model.IloscRzedow;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete")]
        public ActionResult Delete(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var sala = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            if (sala is null)
                return NotFound();
            return View(sala);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZabronDostepu();
            var sala = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            _context.Remove(sala);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Details/{id}")]
        public ActionResult Details(int? id)
        {
            ZabronDostepu();
            if (id is null)
                return NotFound();
            var sala = _context.SaleKinowe.FirstOrDefault(q => q.Id == id);
            if (sala is null)
                return NotFound();
            return View(sala);
        }

        public void ZabronDostepu()
        {
            if (!User.IsInRole("Admin")) HttpContext.Response.Redirect("/");
        }
    }
}
