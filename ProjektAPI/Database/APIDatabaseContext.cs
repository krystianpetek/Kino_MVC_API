using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;

namespace ProjektAPI.Database
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
            modelBuilder.Entity<EmisjaModel>().HasKey(x => x.Id);
            modelBuilder.Entity<EmisjaModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<EmisjaModel>().HasOne(x => x.Sala);
            modelBuilder.Entity<EmisjaModel>().HasOne(x => x.Film);
            modelBuilder.Entity<EmisjaModel>().HasMany(x => x.Rezerwacje).WithOne(x => x.Emisja);

            modelBuilder.Entity<FilmModel>().HasKey(x => x.Id);
            modelBuilder.Entity<FilmModel>().HasIndex(x => x.Id).IsUnique();

            modelBuilder.Entity<KlientModel>().HasKey(x => x.Id);
            modelBuilder.Entity<KlientModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<KlientModel>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<KlientModel>().HasOne(x => x.Uzytkownik).WithOne(x => x.Klient);

            modelBuilder.Entity<RezerwacjaModel>().HasKey(x => x.Id);
            modelBuilder.Entity<RezerwacjaModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<RezerwacjaModel>().HasOne(x => x.Klient).WithMany(x => x.Rezerwacje);
            modelBuilder.Entity<RezerwacjaModel>().HasOne(x => x.Emisja).WithMany(x => x.Rezerwacje);

            modelBuilder.Entity<SalaModel>().HasKey(x => x.Id);
            modelBuilder.Entity<SalaModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<SalaModel>().HasIndex(x => x.NazwaSali).IsUnique();

            modelBuilder.Entity<UzytkownikModel>().HasKey(x => x.Id);
            modelBuilder.Entity<UzytkownikModel>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<UzytkownikModel>().HasIndex(x => x.Login).IsUnique();

            modelBuilder.Entity<ZajeteMiejsca>().HasKey(x => x.Id);
            modelBuilder.Entity<ZajeteMiejsca>().HasIndex(x => x.Id).IsUnique();
        }
    }
}