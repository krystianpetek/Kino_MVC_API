using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class FilmModel
    {
        [Required, Display(Name = "Lp.")]
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
        public string CzasTrwania { get; set; }
        [Required, Display(Name = "Kwota")]
        public float Cena { get; set; }
    }
    public enum Wiek
    {
        BezOgraniczen, Od7lat, Od12lat, Od16lat, Od18lat
    }
}
