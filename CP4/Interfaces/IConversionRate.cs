using CP4.HTTPObjects;


namespace CP4.Interfaces
{
        public interface IConversionRate
        {
            Task<ExchangeRateResponse> GetUsdRateAsync();
        }
    }

