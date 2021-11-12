using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pfm.Commands;

namespace pfm.Controllers
{
    [ApiController]
    [Route("/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController (ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/import")]
        public IActionResult ImportCategories ([FromBody] CategoryCsvCommand command)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetListPEMCategories ([FromQuery] string ParentId)
        {
            return Ok();
        }

    }
}