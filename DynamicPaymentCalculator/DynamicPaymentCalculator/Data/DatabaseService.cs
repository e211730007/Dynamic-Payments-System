using Microsoft.Data.SqlClient;
using System.Data;

namespace DynamicPaymentCalculator.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void BulkInsertData(DataTable table)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "dbo.t_data";
                    bulkCopy.BatchSize = 100000;
                    bulkCopy.BulkCopyTimeout = 0;

                    bulkCopy.ColumnMappings.Add(0, 1); // a -> a
                    bulkCopy.ColumnMappings.Add(1, 2); // b -> b
                    bulkCopy.ColumnMappings.Add(2, 3); // c -> c
                    bulkCopy.ColumnMappings.Add(3, 4); // d -> d

                    try
                    {
                        bulkCopy.WriteToServer(table);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("שגיאה בהזרקה ל-SQL: " + ex.Message);
                    }
                }
            }
        }
    }
}