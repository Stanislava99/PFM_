using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace pfm.Controllers
{
    [ApiController]
    [Route("/v1/analytics")]
    public class AnalyticsController : Controller
    {
        private readonly ILogger<AnalyticsController> _logger;

        public AnalyticsController(ILogger<AnalyticsController> logger){
            _logger = logger;
        }

        // [HttpGet("/spending-analytics")]
        // public async Task<ActionResult> SpendingByCategory ([FromQuery] string catcode, [FromQuery] string startDate,[FromQuery] string endDate, [FromQuery] string description) 
        // {
            
        // }
    }
}