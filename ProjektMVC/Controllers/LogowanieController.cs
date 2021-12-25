using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProjektMVC.Models;
using Microsoft.AspNetCore.Authorization;
using ProjektAPI.Models;

namespace ProjektMVC.Controllers
{
    public class LogowanieController : Controller
    {
        private APIDatabaseContext _context;

        private readonly List<UzytkownikModel> _uzytkownicy;
        public LogowanieController(APIDatabaseContext context)
        {
            _context = context;
            _uzytkownicy = _context.Login.ToList();
            if(!_context.Klienci.Any())
            {
                _context.Klienci.Add(new KlientModel()
                {
                    Imie = "Krystian",
                    Nazwisko = "Petek",
                    DataUrodzenia = new System.DateTime(1998, 10, 06),
                    Miasto = "Koziniec",
                    Ulica = "2",
                    NumerTelefonu = "884284782",
                    KodPocztowy = "34-106",
                    Email = "krystianpetek2@gmail.com",
                    Uzytkownik = new UzytkownikModel()
                    {
                        Login = "krystianpetek",
                        Haslo = "qwerty123",
                        RodzajUzytkownika = Rola.Admin
                    }
                });
                _context.SaveChanges();
            }
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        } 

        public IActionResult Login(string url = "/")
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.Response.Redirect("/");
            LoginModel model = new()
            {
                URL = url
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var uzytkownik = _uzytkownicy.Where(x => x.Login == model.Login && x.Haslo == model.Haslo).FirstOrDefault();
                if(uzytkownik == null)
                {
                    ViewBag.Message = "Provided crediential is not valid.";
                    return View(model);
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, uzytkownik.Id.ToString()),
                        new Claim(ClaimTypes.Name, uzytkownik.Login),
                        new Claim(ClaimTypes.Role, uzytkownik.RodzajUzytkownika.ToString())

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        principal, new AuthenticationProperties() { IsPersistent = model.PamietajMnie });

                    return LocalRedirect(model.URL);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            //logout and remove cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

    }
}
