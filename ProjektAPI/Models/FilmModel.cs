using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class FilmModel
    {
        public int Id { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public string Gatunek { get; set; }
        [Required]
        public Wiek OgraniczeniaWiek { get; set; }
        [Required]
        public string CzasTrwania { get; set; }
        [Required]
        public float Cena { get; set; }
    }
    public enum Wiek
    {
        BezOgraniczen, Od7lat, Od12lat, Od16lat, Od18lat
    }
}
