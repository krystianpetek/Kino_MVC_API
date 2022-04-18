using Microsoft.EntityFrameworkCore;

namespace ProjektAPI.Models
{
    public class APIDatabaseContext : DbContext
    {
        public APIDatabaseContext(DbContextOptions<APIDatabaseContext> options) : base(options)
        { }

        public DbSet<FilmModel> Filmy { get; set; }
        public DbSet<KlientModel> Klienci { get; set; }
        public DbSet<SalaModel> SaleKinowe { get; set; }
        public DbSet<UzytkownikModel> Login { get; set; }
        public DbSet<EmisjaModel> Emisja { get; set; }
        public DbSet<RezerwacjaModel> Rezerwacja { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UzytkownikModel>().HasIndex(x => x.Login).IsUnique();
            modelBuilder.Entity<KlientModel>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<SalaModel>().HasIndex(x => x.NazwaSali).IsUnique();
        }

        //private string _dbConnect = "Server=(localdb)\\mssqllocaldb;Database=KinoDB;Trusted_Connection=True";
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_dbConnect);
        //}
    }
}
