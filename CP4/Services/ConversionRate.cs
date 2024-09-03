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
            // Captura a URL da API a partir do appsettings.json
            _apiUrl = options.Value.BaseUrl;
        }

        public async Task<ExchangeRateResponse> GetUsdRateAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Falha na chamada à API de câmbio.");
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            };

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<ExchangeRateResponse>(json, options);

            return obj;
            //return exchangeRateData?.ConversionRates.BRL ?? 0;
        }
    }
}

