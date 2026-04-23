using Payments.API.Data;
using Payments.API.Models;

namespace Payments.API.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repo;
        public LogService(ILogRepository repo) => _repo = repo;

        public List<CalculationLog> GetComparisonData() => _repo.GetAllLogs();
    }
}