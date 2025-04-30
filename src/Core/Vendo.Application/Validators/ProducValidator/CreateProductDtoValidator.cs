
using FluentValidation;
using Vendo.Application.DTOs.Products;

namespace Vendo.Application.FluentValidator.ProducValidator
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    { 
        public const int NAME_MAX_LENGTH=100;
       
        public const decimal PRICE_MAX =9999.99m;
        public const decimal PRICE_MIN = 3;
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Name Required")
            .MaximumLength(NAME_MAX_LENGTH)
                .WithMessage("Name must contains max 100 symbols");

            RuleFor(p => p.mainPhoto).Must(mp => mp.ContentType.Contains("/image") && mp.Length > 1024 * 2)
                .WithMessage("Size or Type is incorrect");

            RuleFor(p => p.Price).NotEmpty()
                    .WithMessage("Price Required")
                .GreaterThanOrEqualTo(PRICE_MIN).LessThanOrEqualTo(PRICE_MAX);

            RuleFor(p => p.Description)
                .NotEmpty()
                    .WithMessage("Description Required");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                    .Must(id => id > 0);

            RuleFor(p => p.BrandId)
                 .NotEmpty()
                     .Must(id => id > 0);

            RuleForEach(p => p.ColorIds)
                .NotEmpty().Must(colorid => colorid > 0);


            RuleForEach(p => p.SizeIds)
             .NotEmpty().Must(sizeid => sizeid > 0);

        }
    }

}
