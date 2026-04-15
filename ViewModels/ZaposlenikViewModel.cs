using System.ComponentModel.DataAnnotations;

namespace SistemZaZaposlenike.ViewModels;

public class ZaposlenikViewModel
{
    public int ZaposlenikID { get; set; }

    [Required(ErrorMessage = "Ime je obavezno.")]
    [StringLength(100, ErrorMessage = "Ime može imati maksimalno 100 karaktera.")]
    public string Ime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Prezime je obavezno.")]
    [StringLength(100, ErrorMessage = "Prezime može imati maksimalno 100 karaktera.")]
    public string Prezime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
    [StringLength(200, ErrorMessage = "Email može imati maksimalno 200 karaktera.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Plata je obavezna.")]
    [Range(1, 99999999.99, ErrorMessage = "Plata mora biti veća od 0.")]
    public decimal PlataMonthly { get; set; }

    [Required(ErrorMessage = "Datum zaposlenja je obavezan.")]
    [DataType(DataType.Date)]
    public DateTime DatumZaposlenja { get; set; }

    [Required(ErrorMessage = "Odjel je obavezan.")]
    [Range(1, int.MaxValue, ErrorMessage = "Odaberite odjel.")]
    public int OdjelID { get; set; }
}