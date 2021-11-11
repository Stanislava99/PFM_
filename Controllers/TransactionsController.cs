using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pfm.Models;
using pfm.Commands;
using System;

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
        public IActionResult GetTransactions([FromQuery] string transactionKind, [FromQuery] DateTime startDate,[FromQuery] DateTime endDate, [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        {
            return Ok();
        }

        [HttpPost("import")]
        public IActionResult CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            return Ok();
        }

        [HttpPost("{id}/split")]
        public IActionResult SplitTransactionById([FromRoute] int id, [FromBody] SplitTransactionCommand command)
        {
            return Ok();
        }

        [HttpPost("{id}/categorize")]
        public IActionResult CategorizeTransactionById([FromRoute] int id, [FromBody] TransactionCategorizeCommand command)
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