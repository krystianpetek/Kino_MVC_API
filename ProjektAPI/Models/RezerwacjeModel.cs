using System;

namespace ProjektAPI.Models
{
    public class RezerwacjeModel
    {
        public int Id { get; set; }
        public bool Zajete { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
        public int EmisjaId { get; set; }
        public virtual EmisjaModel Emisja { get; set;}
        public int KlientId { get; set; }
        public virtual KlientModel Klient { get; set; }
    }
}