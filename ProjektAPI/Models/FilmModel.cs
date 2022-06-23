using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjektAPI.Models
{
    public class FilmModel
    {
        [Required, Display(Name = "ID")]
        //public Guid Id { get; set; }
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

        [JsonIgnore]
        public virtual EmisjaModel Emisja { get; set; }

    }
}