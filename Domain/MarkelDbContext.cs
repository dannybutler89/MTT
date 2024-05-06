using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class MarkelDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimType> ClaimType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MarkelInsurance");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasKey(pk => pk.Id);

            modelBuilder.Entity<Company>()
                .HasMany(m => m.Claims);

            modelBuilder.Entity<ClaimType>()
                .HasKey(pk => pk.Id);
            
            modelBuilder.Entity<Claim>()
                .HasOne(p => p.Company)
                .WithMany(o => o.Claims)
                .HasForeignKey(fk => fk.CompanyId);

            modelBuilder.Entity<Claim>()
                .HasOne(p => p.ClaimType)
                .WithMany(o => o.Claims)
                .HasForeignKey(fk => fk.ClaimTypeId);
        }
    }
}
