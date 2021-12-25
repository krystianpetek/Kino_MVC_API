using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class UzytkownikModel
    {
        [Required]
        public int Id { get; set; }
        [Required, MinLength(5), StringLength(20)]
        public string Login { get; set; }
        [Required, MinLength(8), StringLength(30)]
        public string Haslo { get; set; }
        [Required]
        public Rola RodzajUzytkownika { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public virtual KlientModel Klient { get; set; }


    }
    public enum Rola
    {
        Admin, Pracownik, Klient
    }
}
