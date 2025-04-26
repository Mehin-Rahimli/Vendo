using Microsoft.AspNetCore.Mvc;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.Products;

namespace Vendo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAll(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();
            GetProductDto productdto = await _service.GetByIdAsync(id);
            if (productdto == null) return NotFound();
            return Ok(productdto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto productDto)
        {
            await _service.CreateAsync(productDto);
            return StatusCode(StatusCodes.Status201Created);
        }



        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateProductDto productDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id, productDto);

            return NoContent();
        }
    }
}


