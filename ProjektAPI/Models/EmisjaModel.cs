using System;

namespace ProjektAPI.Models
{
    public class EmisjaModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int SalaId { get; set; }
        // jesli dataemisji juz mineła to bład
        public DateTime Data { get; set; }
        public virtual FilmModel? Film { get; set; }
        public virtual SalaModel? Sala { get; set; }
    }
}
