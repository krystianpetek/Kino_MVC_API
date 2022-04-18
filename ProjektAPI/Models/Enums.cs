using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public enum Wiek
    {
        [Display(Name = "Bez ograniczeń")]
        BezOgraniczen,

        [Display(Name = "Od 7 lat")]
        Od7lat,

        [Display(Name = "Od 12 lat")]
        Od12lat,

        [Display(Name = "Od 16 lat")]
        Od16lat,

        [Display(Name = "Od 18 lat")]
        Od18lat
    }

    public enum Rola
    {
        [Display(Name = "Administrator")]
        Admin,

        [Display(Name = "Pracownik")]
        Pracownik, 

        [Display(Name = "Klient")]
        Klient
    }
}
