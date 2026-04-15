namespace SistemZaZaposlenike.Services;

public interface IValutaService
{
    Task<decimal?> KonvertujIzBamAsync(decimal iznosBam, string ciljnaValuta);
}