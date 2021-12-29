using System;
using System.ComponentModel.DataAnnotations;

namespace ProjektMVC.Models
{
    public class AktualnieEmitowaneFilmy
    {
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "Data i godzina")]
        public DateTime Data { get; set; }
        [Required, Display(Name = "Nazwa filmu")]
        public string NazwaFilmu { get; set; }
    }
}
