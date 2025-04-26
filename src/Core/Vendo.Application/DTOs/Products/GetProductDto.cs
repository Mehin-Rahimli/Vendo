

namespace Vendo.Application.DTOs.Products
{
    public record GetProductDto(int Id, string Name, decimal Price, BrandItemDto Category,BrandItemDto Brand,
     decimal DiscountPrice, string Decription,bool Gender,
   IEnumerable<SizeItemDto> Sizes,
   IEnumerable<ColorItemDto> Colors
 );
}
