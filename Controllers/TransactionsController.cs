using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using pfm.Models;
using pfm.Commands;
using AutoMapper;
using pfm.Services;
using pfm.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using pfm.Helper;

 namespace pfm.Controllers
{
    [ApiController]
    [Route("/v1/transactions")]
    public class TransactionsController : ControllerBase 
    {

        private readonly ILogger<TransactionsController> _logger;
        
        private readonly ITransactionService _transactionsService;
        private readonly IMapper _mapper;
        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService transactionService, IMapper mapper) 
        {
            _logger = logger;
            _transactionsService = transactionService;
            _mapper = mapper;
        }
        
        // api/transactions/ImportTransactions
        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactions()
        {
            var request = HttpContext.Request;
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            var status = await _transactionsService.AddTransaction(request);
           
            if (status)
            {
                var importMcc = await _transactionsService.ImportMccCodes();
                if(importMcc)
                return StatusCode(201);
            }

            return BadRequest("Error while inserting the transactions");
            
        }


        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] QueryParams transactionsParams)
        {
          
            var status = await _transactionsService.GetPagedListTransactions(transactionsParams);

            Response.AddPagination(status.CurrentPage, status.PageSize, status.TotalCount, status.TotalPages);

            return Ok(status);
        
        }

        [HttpPost("{id}/split")]
        public IActionResult SplitTransactionById([FromRoute] string id, [FromBody] SplitTransactionCommand command)
        {
            
            // var splitParams = new SpltParametars();
            // var transactionId = "10124603";
            // var amount = new List<double>();
            // var catId = new List<string>();
            // amount.Add(1);
            // amount.Add(6);
            // catId.Add("A");
            // catId.Add("B");


            var status = _transactionsService.Split(id, command.splits);
            if (status.Result)
                return Ok("Transaction succesfully splitted");

            return BadRequest();
        }

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransactionById([FromRoute] string id, [FromBody] TransactionCategorizeCommand command)
        {
            var result = await _transactionsService.Categorize(id, command.CatCode);

            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("auto-categorie")]
        public IActionResult AutoCategorizeTransaction()
        {
            return Ok();
        }
       
    }
}