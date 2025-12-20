using Microsoft.EntityFrameworkCore;
using DataEntity.Data;

namespace DataEntity
{
    public class LekarContext : DbContext
    {
        public DbSet<Pacienti> Pacienti { get; set; } = null!;
        public DbSet<Lekarskazprava> LekarskeZpravy { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=DatabazePacienti;" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=True").UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pacienti>()
     .HasMany(p => p.LekarskeZpravy) // Pacient má MNOHO zpráv
     .WithOne(r => r.Pacient)        // Zpráva má JEDNOHO pacienta
     .HasForeignKey(r => r.PacientId);
        }
    }
}