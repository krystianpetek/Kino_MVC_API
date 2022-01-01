using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class RezerwacjaModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, Display(Name = "Rząd")]
        public int Rzad { get; set; }
        [Required, Display(Name = "Miejsce")]
        public int Miejsce { get; set; }
        [Required, Display(Name = "EmisjaID")]
        public int EmisjaId { get; set; }
        public virtual EmisjaModel Emisja { get; set;}
        [Required, Display(Name = "KlientID")] 
        public int KlientId { get; set; }
        public virtual KlientModel Klient { get; set; }
    }
}