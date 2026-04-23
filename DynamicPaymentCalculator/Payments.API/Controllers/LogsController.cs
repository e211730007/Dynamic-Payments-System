using Microsoft.AspNetCore.Mvc;
using Payments.API.Services; // וודאי שזה השם של תיקיית ה-Services שלך
using Payments.API.Models;   // וודאי שזה השם של תיקיית המודלים שלך

namespace Payments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        // הזרקה של הממשק דרך ה-Constructor
        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public IActionResult GetAllLogs()
        {
            try
            {
                // קורא ל-BL, שקורא ל-DAL, שקורא ל-SQL
                var data = _logService.GetComparisonData();

                if (data == null || !data.Any())
                {
                    return NotFound("לא נמצאו נתונים בטבלת הלוג");
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה בשליפת הנתונים: {ex.Message}");
            }
        }

    }
}