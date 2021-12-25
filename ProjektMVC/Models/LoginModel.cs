﻿using System.ComponentModel.DataAnnotations;
using System;

namespace ProjektMVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Haslo { get; set; }

        [Display(Name = " Nie wylogowywuj")]
        public bool PamietajMnie { get; set; }

        public string URL { get; set; }
    }
}
