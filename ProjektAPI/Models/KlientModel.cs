using ProjektAPI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Models
{
    public class KlientModel
    {
        [Required, Display(Name = "ID")]
        //public int Id { get; set; }
        public Guid Id { get; set; }

        [Required, Display(Name = "Imię"), StringLength(30)]
        public string Imie { get; set; }

        [Required, Display(Name = "Nazwisko"), StringLength(50)]
        public string Nazwisko { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Data urodzenia"), WalidacjaWieku]
        public DateTime DataUrodzenia { get; set; }

        [Phone, Display(Name = "Numer telefonu")]
        public string NumerTelefonu { get; set; }

        [Display(Name = "Miasto"), StringLength(60)]
        public string Miasto { get; set; }

        [Display(Name = "Ulica i numer")]
        public string Ulica { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string KodPocztowy { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        //public int UzytkownikId { get; set; }
        public Guid UzytkownikId { get; set; }
        public virtual UzytkownikModel Uzytkownik { get; set; }

        public virtual List<RezerwacjaModel> Rezerwacje { get; set; }
    }
}