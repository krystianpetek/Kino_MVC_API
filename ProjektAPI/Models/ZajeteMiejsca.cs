using System;

namespace ProjektAPI.Models
{
    public class ZajeteMiejsca
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmisjaId { get; set; }
        public int Rzad { get; set; }
        public int Miejsce { get; set; }
    }
}