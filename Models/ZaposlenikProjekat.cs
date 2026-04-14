using System.ComponentModel.DataAnnotations;

namespace SistemZaZaposlenike.Models;

public class ZaposlenikProjekat
{
    public int ZaposlenikID { get; set; }
    public Zaposlenik? Zaposlenik { get; set; }

    public int ProjekatID { get; set; }
    public Projekat? Projekat { get; set; }

    [StringLength(100)]
    public string? Uloga { get; set; }

    public int SatiNaTjednu { get; set; }
}