
using FluentValidation;
using Vendo.Application.DTOs;

namespace Vendo.Application.FluentValidator.SizeDtoValidator
{
   public class CreateSizeDtoValidator:AbstractValidator<CreateSizeDto>
    {
        public CreateSizeDtoValidator()
        {
            RuleFor(s=>s.Name).NotEmpty()
                    .WithMessage("Name Required")
                    .MaximumLength(100)
                      .WithMessage("Must contains max 100 symbols")
                .Matches(@"^[A-Za-z\s]*$");
        }
    }
}
