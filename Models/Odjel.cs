using System.ComponentModel.DataAnnotations;

namespace SistemZaZaposlenike.Models;

public class Odjel
{
    public int OdjelID { get; set; }

    [Required]
    [StringLength(100)]
    public string Naziv { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Lokacija { get; set; }

    public ICollection<Zaposlenik> Zaposlenici { get; set; } = new List<Zaposlenik>();
}