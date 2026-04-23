using System.Data.SqlClient;
using Payments.API.Models;

namespace Payments.API.Data
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;
        public LogRepository(IConfiguration config) => _connectionString = config.GetConnectionString("DefaultConnection");

        public List<CalculationLog> GetAllLogs()
        {
            var logs = new List<CalculationLog>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT method, run_time FROM t_log ORDER BY log_id DESC", conn);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new CalculationLog
                        {
                            Method = reader["method"].ToString(),
                            RunTime = Convert.ToDouble(reader["run_time"])
                        });
                    }
                }
            }
            return logs;
        }
    }
}