using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class EmisjaModel
    {
        [Required]
        public Guid Id { get; set; }
        //public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DataType(DataType.Time)]
        public DateTime Godzina { get; set; }

        public Guid FilmId { get; set; }
        //public int FilmId { get; set; }
        public virtual FilmModel Film { get; set; }

        public Guid SalaId { get; set; }
        //public int SalaId { get; set; }

        public virtual SalaModel Sala { get; set; }

        public virtual List<RezerwacjaModel> Rezerwacje { get; set; }
    }
}