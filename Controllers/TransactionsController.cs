using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pfm.Models;

 namespace pfm.Controllers
{
    [ApiController]
    [Route("/v1/transactions")]
    public class TransactionsController : ControllerBase 
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger) 
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTransactions([FromQuery] string transactionKind, [FromQuery] string startDate,[FromQuery] string endDate, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        {
            return Ok();
        }

        [HttpPost("import")]
        public IActionResult CreateTransaction()
        {
            return Ok();
        }

        [HttpPost("{id}/split")]
        public IActionResult SplitTransactionById([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost("{id}/categorize")]
        public IActionResult CategorizeTransactionById([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPost("auto-categorie")]
        public IActionResult AutoCategorizeTransaction()
        {
            return Ok();
        }
    }
}