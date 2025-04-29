using FluentValidation;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.DTOs;



namespace Vendo.Application.FluentValidator.ColorDtoValidator
{
    public class CreateColorDtoValidator:AbstractValidator<CreateColorDto>
    {
        private readonly IColorRepository _repository;

        public CreateColorDtoValidator(IColorRepository repository)
        {
            _repository = repository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(2)
                .Matches(@"[A-Za-z\s0-9]*$");
        }
        //public async  Task<bool> CheckName(string name,CancellationToken token)
        //{
        //    return !await _repository.AnyAsync(c => c.Name == name);
        //}
    }
}
