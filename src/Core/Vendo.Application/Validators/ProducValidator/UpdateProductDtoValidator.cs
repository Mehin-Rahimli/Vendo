using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Vendo.Application.DTOs.Products;

namespace Vendo.Application.FluentValidator.ProducValidator
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public const int NAME_MAX_LENGTH = 100;
        public const decimal PRICE_MAX = 9999.99m;
        public const decimal PRICE_MIN = 3;
        public UpdateProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .MaximumLength(NAME_MAX_LENGTH)
                    .WithMessage("Name must contains max 100 symbols");
         
            RuleFor(p => p.Description)
                .NotEmpty();
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(PRICE_MIN)
                .LessThanOrEqualTo(PRICE_MAX);

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .Must(categoryId => categoryId > 0);

            RuleFor(p => p.BrandId)
           .NotEmpty()
           .Must(brandId => brandId > 0);

            RuleForEach(p => p.ColorIds)
                .NotEmpty()
                .Must(colorId => colorId > 0)
                    .WithMessage("Wrong Color id");

            RuleForEach(p => p.SizeIds)
              .NotEmpty()
              .Must(sizeId => sizeId > 0)
                 .WithMessage("Wrong Size Id");
          
            

        }
    }
}
