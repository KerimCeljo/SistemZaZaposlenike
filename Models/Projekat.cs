using System.ComponentModel.DataAnnotations;

namespace SistemZaZaposlenike.Models;

public class Projekat
{
    public int ProjekatID { get; set; }

    [Required]
    [StringLength(200)]
    public string Naziv { get; set; } = string.Empty;

    [Range(0, 999999999999.99)]
    public decimal? BudzetUSD { get; set; }

    public DateTime? DatumPocetak { get; set; }
    public DateTime? DatumKraj { get; set; }

    public ICollection<ZaposlenikProjekat> ZaposlenikProjekti { get; set; } = new List<ZaposlenikProjekat>();
}