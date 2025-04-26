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

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Required")
                .MaximumLength(100).WithMessage("Must contains max 100 symbols")
                .Matches(@"^[A-Za-z\s0-9]*$");
                //.MustAsync(CheckName);
        }
        public async Task<bool> CheckName(string name,CancellationToken token)
        {
            return !await _repository.AnyAsync(x => x.Name == name);
        }
    }
}
