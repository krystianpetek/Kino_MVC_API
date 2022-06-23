namespace ProjektAPI.RequestModels
{
    public class FilmModelRequest
    {
        //public Guid Id { get; set; }
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public string Opis { get; set; }

        public string Gatunek { get; set; }

        public string OgraniczeniaWiek { get; set; }

        public int CzasTrwania { get; set; }

        public float Cena { get; set; }
    }
}
