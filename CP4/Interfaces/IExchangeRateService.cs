using CP4.Services;

namespace CP4.Interfaces
{
    public interface IExchangeRateService
    {
        Task<decimal> GetUsdToBrlRateAsync();
    }
}