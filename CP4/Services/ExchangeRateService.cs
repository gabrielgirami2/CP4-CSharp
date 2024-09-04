using CP4.Interfaces;
using System.Threading.Tasks;

namespace CP4.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IConversionRate _conversionRate;

        public ExchangeRateService(IConversionRate conversionRate)
        {
            _conversionRate = conversionRate;
        }

        public async Task<decimal> GetUsdToBrlRateAsync()
        {
            var result = await _conversionRate.GetUsdRateAsync();
            return result.ConversionRates.BRL;
        }
    }
}
