using System.Text.Json;

namespace SistemZaZaposlenike.Services;

public class ValutaService : IValutaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ValutaService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<decimal?> KonvertujIzBamAsync(decimal iznosBam, string ciljnaValuta)
    {
        try
        {
            var baseUrl = _configuration["ExchangeRateApi:BaseUrl"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                return null;

            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty("rates", out var ratesElement))
                return null;

            if (!ratesElement.TryGetProperty(ciljnaValuta.ToUpper(), out var rateElement))
                return null;

            var rate = rateElement.GetDecimal();

            return Math.Round(iznosBam * rate, 2);
        }
        catch
        {
            return null;
        }
    }
}