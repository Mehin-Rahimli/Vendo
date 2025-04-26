using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendo.Application.Abstractions.Services;
using Vendo.Application;

namespace Vendo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;
        private readonly IValidator<CreateBrandDto> _validator;

        public BrandsController(IBrandService service, IValidator<CreateBrandDto> validator)
        {

            _service = service;
            _validator = validator;
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
            var brandDto = await _service.GetByIdAsync(id);
            if (brandDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, brandDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandDto brandDto)
        {


            ValidationResult result = await _validator.ValidateAsync(brandDto);
            if (!result.IsValid)
            {
                foreach (ValidationFailure error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
            await _service.CreateAsync(brandDto);

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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateBrandDto brandDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id,brandDto);

            return NoContent();
        }
   
}
}
