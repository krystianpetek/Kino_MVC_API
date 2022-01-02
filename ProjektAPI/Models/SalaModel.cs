using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class SalaModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }
        [Required, Display(Name ="Nazwa sali")]
        public string NazwaSali { get; set; }
        [Required, Range(5, 15), Display(Name = "Ilość rzędów")]
        public int IloscRzedow { get; set; }
        [Required, Range(10, 20), Display(Name = "ilość miejsc")]
        public int IloscMiejsc { get; set; }
        [Display(Name = "Liczba dostępnych miejsc")]
        public int LiczbaMiejsc => IloscMiejsc * IloscRzedow;
    }
}
