using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using pfm.Services;
using System.Collections.Generic;
using AutoMapper;

using pfm.Database.Entities;


namespace pfm.Controllers
{
    [ApiController]
    [Route("/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;
        public CategoriesController (ILogger<CategoriesController> logger, ICategoriesService categoriesService, IMapper mapper)
        {
            _logger = logger;
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCategories()
        {
            var request = HttpContext.Request;
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            var status = await _categoriesService.AddCategories(request);
            if (status) return StatusCode(201);

            return BadRequest("Error while inserting the categories");
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories ([FromQuery] string ParentId)
        {
            List<Categories> categories = new List<Categories>();
            
            var result = await _categoriesService.GetCategories(ParentId);

            foreach(var category in result)
            {
                Categories c = new Categories();

                categories.Add(_mapper.Map(category, c));
            }

            return Ok(categories);
        }

    }
}