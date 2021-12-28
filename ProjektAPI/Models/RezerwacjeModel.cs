using System;

namespace ProjektAPI.Models
{
    public class RezerwacjeModel
    {
        public int Id { get; set; }
        public bool Zajete { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
        public int IdEmisjiFilmu { get; set; }
        public virtual EmisjaFilmuModel EmisjaFilmu { get; set;}
    }
}