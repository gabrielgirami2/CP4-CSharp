using Microsoft.AspNetCore.Mvc;

namespace CP4.Interfaces
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public interface IExchangeController
    {
        Task<IActionResult> GetUsdToBrl();
    }
}
