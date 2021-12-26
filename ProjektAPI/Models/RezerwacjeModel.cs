﻿using System;

namespace ProjektAPI.Models
{
    public class RezerwacjeModel
    {
        public int Id { get; set; }
        public bool Zajete { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
        public int LiczbaPorzadkowa { get; set; } // rzad * miejsce
        public DateTime GodzinaEmisji { get; set; }
        public int IdSaleKinowe { get; set; }
        public int IdFilm { get; set; }
        public int IdKlient { get; set; }
        public virtual SalaModel SaleKinowe { get; set; }
        public virtual FilmModel Filmy { get; set; }
        public virtual KlientModel Klienci{ get; set; }
    }
}