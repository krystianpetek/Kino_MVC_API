using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class FilmModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, StringLength(80), Display(Name = "Nazwa")]
        public string Nazwa { get; set; }
        [Required, StringLength(255), Display(Name = "Opis")]
        public string Opis { get; set; }
        [Required, Display(Name = "Gatunek")]
        public string Gatunek { get; set; }
        [Required, Display(Name = "Wiek")]
        public Wiek OgraniczeniaWiek { get; set; }
        [Required, Display(Name = "Czas trwania")]
        public int CzasTrwania { get; set; }
        [Required, Display(Name = "Kwota")]
        public float Cena { get; set; }
    }
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
}
