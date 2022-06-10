using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjektAPI.Models
{
    public class RezerwacjaModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, Display(Name = "Rząd")]
        public int Rzad { get; set; }

        [Required, Display(Name = "Miejsce")]
        public int Miejsce { get; set; }

        public Guid EmisjaId { get; set; }
        public virtual EmisjaModel Emisja { get; set; }

        public Guid KlientId { get; set; }
        public virtual KlientModel Klient { get; set; }
    }
}