using System;
using System.Data;
using DynamicPaymentCalculator.Data;

namespace DynamicPaymentCalculator.Services
{
    public class DataGeneratorService : IDataGeneratorService
    {
        private readonly Random _random = new Random(); 

        public DataTable GenerateData(int count)
        {
            var table = new DataTable();
            table.Columns.Add("data_id", typeof(int));
            table.Columns.Add("a", typeof(double));
            table.Columns.Add("b", typeof(double));
            table.Columns.Add("c", typeof(double));
            table.Columns.Add("d", typeof(double));

            for (int i = 0; i < count; i++)
            {
                table.Rows.Add(
                    DBNull.Value,
                    _random.NextDouble() * 100,
                    _random.NextDouble() * 100,
                    _random.NextDouble() * 100,
                    _random.NextDouble() * 100
                );
            }
            return table;
        }
    }

}