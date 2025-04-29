using FluentValidation;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.DTOs;

namespace Vendo.Application.FluentValidator
{
     public class CreateCategoryDtoValidator:AbstractValidator<CreateCategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryDtoValidator(ICategoryRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Data required")
                .MaximumLength(100).WithMessage("Characters should be less than 100")
                .MinimumLength(3)
                .Matches(@"[A-Zaz-z\s0-9]*$");
        }
        //public async Task<bool> CheckName(string name,CancellationToken token)
        //{
        //    return !await _repository.AnyAsync(x => x.Name == name);
        //}
    }
}
