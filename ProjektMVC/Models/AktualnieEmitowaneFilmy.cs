using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektMVC.Models
{
    public class AktualnieEmitowaneFilmy
    {
        [Required]
        public Guid Id { get; set; }
        //public int Id { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Required, DataType(DataType.Time), Display(Name = "Godzina")]
        public DateTime Godzina { get; set; }

        [Required, Display(Name = "Nazwa filmu")]
        public string NazwaFilmu { get; set; }
    }
}