using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Vendo.Application;
using Vendo.Application.Abstractions.Services;


namespace Vendo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {


        private readonly ICategoryService _service;
        private readonly IValidator<CreateCategoryDto> _validator;

        public CategoriesController(ICategoryService service, IValidator<CreateCategoryDto> validator)
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
            var categoryDto = await _service.GetByIdAsync(id);
            if (categoryDto == null) return NotFound();
            return StatusCode(StatusCodes.Status200OK, categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto categoryDto)
        {


            ValidationResult result = await _validator.ValidateAsync(categoryDto);
            if (!result.IsValid)
            {
                foreach (ValidationFailure error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
                }
                await _service.CreateAsync(categoryDto);

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
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDto categoryDto)
        {
            if (id < 1) return BadRequest();

            await _service.UpdateAsync(id, categoryDto);

            return NoContent();
        }
    }
}
