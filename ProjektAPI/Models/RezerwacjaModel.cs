using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class RezerwacjaModel
    {
        [Display(Name = "ID")]
        //public Guid Id { get; set; }
        public int Id { get; set; }

        [Required, Display(Name = "Rząd")]
        public int Rzad { get; set; }

        [Required, Display(Name = "Miejsce")]
        public int Miejsce { get; set; }

        public int EmisjaId { get; set; }
        //public Guid EmisjaId { get; set; }
        public virtual EmisjaModel Emisja { get; set; }

        public int KlientId { get; set; }
        //public Guid KlientId { get; set; }
        public virtual KlientModel Klient { get; set; }
    }
}