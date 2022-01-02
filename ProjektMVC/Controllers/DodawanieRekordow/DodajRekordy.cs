using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Models;

namespace ProjektMVC.Controllers.DodawanieRekordow
{
    public class DodajRekordy : Controller
    {
        private readonly APIDatabaseContext _context;
        public DodajRekordy(APIDatabaseContext context)
        {
            _context = context;
        }
        //public IActionResult SaleKinowe()
        //{
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 1", IloscMiejsc = 10, IloscRzedow = 6 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 2", IloscMiejsc = 12, IloscRzedow = 8 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 3", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 4", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 5", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 6", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 7", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala 8", IloscMiejsc = 10, IloscRzedow = 10 });
        //    _context.SaleKinowe.Add(new SalaModel() { NazwaSali = "Sala Prywatna", IloscMiejsc = 3, IloscRzedow = 5 });
        //    _context.SaveChanges();
        //}
    }
}
