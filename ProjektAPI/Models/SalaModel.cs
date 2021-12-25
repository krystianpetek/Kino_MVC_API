using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class SalaModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NazwaSali { get; set; }
        [Required, Range(6, 10)]
        public int IloscRzedow { get; set; }
        [Required, Range(10, 15)]
        public int IloscMiejsc { get; set; }
        public int LiczbaMiejsc => IloscMiejsc * IloscRzedow;
    }
}
