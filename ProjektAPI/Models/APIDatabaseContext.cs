using Microsoft.EntityFrameworkCore;

namespace ProjektAPI.Models
{
    public class APIDatabaseContext : DbContext
    {
        public APIDatabaseContext (DbContextOptions<APIDatabaseContext> options) : base(options)
        { }
        private string _dbConnect = "Server=(localdb)\\mssqllocaldb;Database=KinoDB;Trusted_Connection=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnect);
        }
        public DbSet<FilmModel> Filmy { get; set; }
        public DbSet<KlientModel> Klienci { get; set; }
        public DbSet<SalaModel> SaleKinowe { get; set; }
        public DbSet<UzytkownikModel> Login{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UzytkownikModel>().HasIndex(x => x.Login).IsUnique();
        }
    } 
}
