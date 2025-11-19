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
                .HasOne(p => p.Lekarskazprava)                         // must match Pacienti property name
                .WithOne(r => r.Pacient)                               // must match Lekarskazprava property
                .HasForeignKey<Lekarskazprava>(r => r.PacientId);      // use FK property on dependent
        }
    }
}