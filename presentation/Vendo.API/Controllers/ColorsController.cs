using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs;
using Vendo.Application;
using FluentValidation;

namespace Vendo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;
        private readonly IValidator<CreateColorDto> _validator;
        public ColorsController(IColorService service, IValidator<CreateColorDto> validator)
        {
            _validator=validator;
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
            var colorDto = await _service.GetByIdAsync(id);
            if (colorDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, colorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateColorDto colorDto)
        {
            await _service.CreateAsync(colorDto);

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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateColorDto colorDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, colorDto);

            return NoContent();
        }
    }
}
