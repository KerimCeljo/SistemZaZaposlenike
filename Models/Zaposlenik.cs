using System.ComponentModel.DataAnnotations;

namespace SistemZaZaposlenike.Models;

public class Zaposlenik
{
    public int ZaposlenikID { get; set; }

    [Required]
    [StringLength(100)]
    public string Ime { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Prezime { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(0, 99999999.99)]
    public decimal PlataMonthly { get; set; }

    [Required]
    public DateTime DatumZaposlenja { get; set; }

    [Required]
    public int OdjelID { get; set; }

    public Odjel? Odjel { get; set; }

    public ICollection<ZaposlenikProjekat> ZaposlenikProjekti { get; set; } = new List<ZaposlenikProjekat>();
}