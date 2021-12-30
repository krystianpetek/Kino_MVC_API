using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class EmisjaModel
    {
        [Required]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int SalaId { get; set; }
        // jesli dataemisji juz mineła to bład
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [DataType(DataType.Time)]
        public DateTime? Godzina { get; set; }
        public virtual FilmModel Film { get; set; }
        public virtual SalaModel Sala { get; set; }
    }
}
