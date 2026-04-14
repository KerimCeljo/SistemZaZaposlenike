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

        modelBuilder.Entity<Odjel>().HasData(
            new Odjel { OdjelID = 1, Naziv = "IT", Lokacija = "Sarajevo" },
            new Odjel { OdjelID = 2, Naziv = "HR", Lokacija = "Sarajevo" },
            new Odjel { OdjelID = 3, Naziv = "Finance", Lokacija = "Sarajevo" }
        );

        modelBuilder.Entity<Projekat>().HasData(
            new Projekat { ProjekatID = 1, Naziv = "Web App", BudzetUSD = 15000, DatumPocetak = new DateTime(2026, 1, 10), DatumKraj = new DateTime(2026, 6, 30) },
            new Projekat { ProjekatID = 2, Naziv = "Mobile App", BudzetUSD = 22000, DatumPocetak = new DateTime(2026, 2, 1), DatumKraj = new DateTime(2026, 8, 15) },
            new Projekat { ProjekatID = 3, Naziv = "API System", BudzetUSD = 18000, DatumPocetak = new DateTime(2026, 3, 5), DatumKraj = new DateTime(2026, 9, 1) },
            new Projekat { ProjekatID = 4, Naziv = "Internal Tool", BudzetUSD = 12000, DatumPocetak = new DateTime(2026, 1, 20), DatumKraj = new DateTime(2026, 5, 20) }
        );

        modelBuilder.Entity<Zaposlenik>().HasData(
            new Zaposlenik { ZaposlenikID = 1, Ime = "Kerim", Prezime = "Celjo", Email = "kerim@test.com", PlataMonthly = 2000, DatumZaposlenja = new DateTime(2024, 1, 15), OdjelID = 1 },
            new Zaposlenik { ZaposlenikID = 2, Ime = "Amir", Prezime = "Hodzic", Email = "amir@test.com", PlataMonthly = 1800, DatumZaposlenja = new DateTime(2024, 3, 10), OdjelID = 1 },
            new Zaposlenik { ZaposlenikID = 3, Ime = "Lejla", Prezime = "Kovacevic", Email = "lejla@test.com", PlataMonthly = 1700, DatumZaposlenja = new DateTime(2023, 11, 5), OdjelID = 2 },
            new Zaposlenik { ZaposlenikID = 4, Ime = "Sara", Prezime = "Basic", Email = "sara@test.com", PlataMonthly = 1600, DatumZaposlenja = new DateTime(2024, 4, 1), OdjelID = 2 },
            new Zaposlenik { ZaposlenikID = 5, Ime = "Adnan", Prezime = "Suljic", Email = "adnan@test.com", PlataMonthly = 2200, DatumZaposlenja = new DateTime(2023, 9, 12), OdjelID = 3 },
            new Zaposlenik { ZaposlenikID = 6, Ime = "Haris", Prezime = "Delic", Email = "haris@test.com", PlataMonthly = 2100, DatumZaposlenja = new DateTime(2024, 2, 20), OdjelID = 3 },
            new Zaposlenik { ZaposlenikID = 7, Ime = "Maja", Prezime = "Ilic", Email = "maja@test.com", PlataMonthly = 1900, DatumZaposlenja = new DateTime(2024, 5, 14), OdjelID = 1 },
            new Zaposlenik { ZaposlenikID = 8, Ime = "Ivan", Prezime = "Peric", Email = "ivan@test.com", PlataMonthly = 1750, DatumZaposlenja = new DateTime(2023, 12, 18), OdjelID = 2 },
            new Zaposlenik { ZaposlenikID = 9, Ime = "Amina", Prezime = "Smajic", Email = "amina@test.com", PlataMonthly = 1650, DatumZaposlenja = new DateTime(2024, 6, 2), OdjelID = 3 },
            new Zaposlenik { ZaposlenikID = 10, Ime = "Nermin", Prezime = "Zukic", Email = "nermin@test.com", PlataMonthly = 2000, DatumZaposlenja = new DateTime(2024, 1, 30), OdjelID = 1 }
        );

        modelBuilder.Entity<ZaposlenikProjekat>().HasData(
            new ZaposlenikProjekat { ZaposlenikID = 1, ProjekatID = 1, Uloga = "Developer", SatiNaTjednu = 40 },
            new ZaposlenikProjekat { ZaposlenikID = 2, ProjekatID = 1, Uloga = "Backend", SatiNaTjednu = 35 },
            new ZaposlenikProjekat { ZaposlenikID = 3, ProjekatID = 2, Uloga = "HR Support", SatiNaTjednu = 20 },
            new ZaposlenikProjekat { ZaposlenikID = 4, ProjekatID = 3, Uloga = "QA", SatiNaTjednu = 25 },
            new ZaposlenikProjekat { ZaposlenikID = 5, ProjekatID = 4, Uloga = "Finance Lead", SatiNaTjednu = 30 }
        );
    }
}