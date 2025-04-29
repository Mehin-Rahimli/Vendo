using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendo.Application.DTOs.Products
{
    public record GetProductDto(
        int Id,
        string Name,
        decimal Price,
        string Decription,
        bool Gender,
        CategoryItemDto Category,
        BrandItemDto Brand,
        decimal DiscountPrice,
        IEnumerable<SizeItemDto> Sizes,
        IEnumerable<ColorItemDto> Colors);
}
