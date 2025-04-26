using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vendo.Application;
using Vendo.Application.Abstractions.Services;

namespace Vendo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _service;
        private readonly IValidator<CreateSizeDto> _validator;
        public SizesController(ISizeService service, IValidator<CreateSizeDto> validator)
        {
            _validator = validator;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();
            var sizeDto = await _service.GetByIdAsync(id);
            if (sizeDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, sizeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSizeDto sizeDto)
        {
            await _service.CreateAsync(sizeDto);

            return StatusCode(StatusCodes.Status201Created);


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSizeDto sizeDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, sizeDto);

            return NoContent();
        }
    }
}
