using Microsoft.EntityFrameworkCore;
using SistemZaZaposlenike.Models;

namespace SistemZaZaposlenike.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Odjel> Odjeli { get; set; }
    public DbSet<Zaposlenik> Zaposlenici { get; set; }
    public DbSet<Projekat> Projekti { get; set; }
    public DbSet<ZaposlenikProjekat> ZaposlenikProjekti { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Zaposlenik>()
            .HasIndex(z => z.Email)
            .IsUnique();

        modelBuilder.Entity<ZaposlenikProjekat>()
            .HasKey(zp => new { zp.ZaposlenikID, zp.ProjekatID });

        modelBuilder.Entity<ZaposlenikProjekat>()
            .HasOne(zp => zp.Zaposlenik)
            .WithMany(z => z.ZaposlenikProjekti)
            .HasForeignKey(zp => zp.ZaposlenikID);

        modelBuilder.Entity<ZaposlenikProjekat>()
            .HasOne(zp => zp.Projekat)
            .WithMany(p => p.ZaposlenikProjekti)
            .HasForeignKey(zp => zp.ProjekatID);
    }
}