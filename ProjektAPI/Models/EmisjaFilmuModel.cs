using System;

namespace ProjektAPI.Models
{
    public class EmisjaFilmuModel
    {
        public int Id { get; set; }
        public int IdFilmu { get; set; }
        public virtual FilmModel Film { get; set; }
        public int IdSali { get; set; }
        public virtual SalaModel SalaKinowa { get; set; }
        public DateTime Data { get; set; }
    }
}
