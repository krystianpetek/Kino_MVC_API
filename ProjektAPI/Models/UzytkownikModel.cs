using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class UzytkownikModel
    {
        [Required, Display(Name = "ID")]
        public int Id { get; set; }

        [Required, MinLength(5), StringLength(20), Display(Name = "Login")]
        public string Login { get; set; }

        [Required, MinLength(8), StringLength(30), Display(Name = " Hasło")]
        public string Haslo { get; set; }

        [Required, Display(Name = "Autoryzacja")]
        public Rola RodzajUzytkownika { get; set; }
    }
}