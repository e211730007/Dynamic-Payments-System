using Payments.API.Models;

namespace Payments.API.Data
{
    public interface ILogRepository
    {
        List<CalculationLog> GetAllLogs();
    }
}