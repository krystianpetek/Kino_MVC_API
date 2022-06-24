using System;

namespace ProjektAPI.Models
{
    public class ZajeteMiejsca
    {
        public Guid Id { get; set; }
        //public int Id { get; set; }
        public Guid EmisjaId { get; set; }
        //public int EmisjaId { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
    }
}