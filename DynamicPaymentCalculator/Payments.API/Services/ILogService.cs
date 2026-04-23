using Payments.API.Models;

namespace Payments.API.Services
{
    public interface ILogService
    {
        List<CalculationLog> GetComparisonData();
    }
}