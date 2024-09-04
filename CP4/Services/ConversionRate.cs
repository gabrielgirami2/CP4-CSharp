using CP4.HTTPObjects;
using Microsoft.Extensions.Options;
using System.Text.Json;
using CP4.Interfaces;

namespace CP4.Services
{
    public class ConversionRate : IConversionRate
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public ConversionRate(HttpClient httpClient, IOptions<ExchangeRateApiSettings> options)
        {
            _httpClient = httpClient;
            _apiUrl = options.Value.BaseUrl; // Ensure that the URL is correctly configured in appsettings.json
        }

        public async Task<ExchangeRateResponse> GetUsdRateAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Failed to call the exchange rate API.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<ExchangeRateResponse>(json, options);

            return obj;
        }
    }
}
