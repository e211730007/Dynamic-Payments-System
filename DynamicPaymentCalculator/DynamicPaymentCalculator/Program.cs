using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DynamicPaymentsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine("--- Starting Payment Calculation System ---");

            p.RunSqlStoredProcedure();

            p.RunDotNetEngine();

            Console.WriteLine("\nAll tasks completed. Press any key to exit...");
            Console.ReadKey();
        }

        public void RunSqlStoredProcedure()
        {
            string connectionString = "Server=DESKTOP-QSADO9D\\SQLEXPRESS;Database=DynamicPaymentsDB;Trusted_Connection=True;Encrypt=False;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("[SQL] Starting Dynamic SQL Engine (100k rows)...");

                    Stopwatch sw = Stopwatch.StartNew();

                    SqlCommand cmd = new SqlCommand("sp_RunAllCalculations", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600; // הגדלת זמן המתנה ל-10 דקות

                    cmd.ExecuteNonQuery();

                    sw.Stop();
                    Console.WriteLine($"[SQL] Success! Time: {sw.Elapsed.TotalSeconds:F2} seconds.");

                    SaveLogToDb(conn, "DB", sw.Elapsed.TotalSeconds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[SQL] Error: " + ex.Message);
                }
            }
        }

        public void RunDotNetEngine()
        {
            string connStr = "Server=DESKTOP-QSADO9D\\SQLEXPRESS;Database=DynamicPaymentsDB;Trusted_Connection=True;Encrypt=False;";
            DataTable dataTable = new DataTable();
            DataTable targilTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("[.NET] Loading 100,000 rows into memory...");

                    SqlDataAdapter daTargil = new SqlDataAdapter("SELECT * FROM t_targil", conn);
                    daTargil.Fill(targilTable);

                    // שליפת 100,000 שורות כדי שהביצועים יהיו מהירים לצורך הדו"ח
                    SqlDataAdapter daData = new SqlDataAdapter("SELECT TOP 100000 a, b, c, d FROM t_data", conn);
                    daData.SelectCommand.CommandTimeout = 600;

                    daData.Fill(dataTable);

                    Console.WriteLine("[.NET] Starting calculation using DataTable.Compute...");
                    Stopwatch sw = Stopwatch.StartNew();

                    foreach (DataRow targilRow in targilTable.Rows)
                    {
                        int id = Convert.ToInt32(targilRow["targil_id"]);
                        string formula = targilRow["targil"].ToString();
                        string condition = targilRow["tnai"].ToString();
                        string formulaFalse = targilRow["targil_false"].ToString();

                        string finalExpression;

                        // אם יש תנאי, נבנה ביטוי מסוג IIF(condition, true_val, false_val)
                        if (!string.IsNullOrEmpty(condition))
                        {
                            finalExpression = $"IIF({condition}, {formula}, {formulaFalse})";
                        }
                        else
                        {
                            finalExpression = formula;
                        }

                        DataColumn calcCol = new DataColumn("Result_" + id, typeof(double), finalExpression);
                        dataTable.Columns.Add(calcCol);
                    }

                    sw.Stop();
                    Console.WriteLine($"[.NET] Success! Time: {sw.Elapsed.TotalSeconds:F2} seconds.");
                    SaveLogToDb(conn, ".NET", sw.Elapsed.TotalSeconds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[.NET] Error: " + ex.Message);
                }
            }
        }

        private void SaveLogToDb(SqlConnection conn, string method, double runTime)
        {
            string getFirstIdQuery = "SELECT TOP 1 targil_id FROM t_targil";
            SqlCommand getFirstIdCmd = new SqlCommand(getFirstIdQuery, conn);
            object firstId = getFirstIdCmd.ExecuteScalar();

            if (firstId != null)
            {
                string query = "INSERT INTO t_log (targil_id, method, run_time) VALUES (@id, @method, @time)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", firstId);
                    cmd.Parameters.AddWithValue("@method", method);
                    cmd.Parameters.AddWithValue("@time", runTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}