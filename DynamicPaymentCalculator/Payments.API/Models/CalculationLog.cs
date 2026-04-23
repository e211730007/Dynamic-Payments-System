using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payments.API.Models
{
    [Table("t_results")] // מקשר לטבלה שיצרת בסקריפט
    public class CalculationLog
    {
        [Key]
        [Column("result_id")] 
        public int Id { get; set; }

        [Column("data_id")]
        public int DataId { get; set; }

        [Column("calculation_result")] 
        public double CalculationResult { get; set; }

        [Column("engine_name")] 
        public string Method { get; set; }

        [Column("calculation_time_ms")] 
        public double RunTime { get; set; }

        [Column("created_at")] 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}