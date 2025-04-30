

using Microsoft.AspNetCore.Http;

namespace Vendo.Application.DTOs.Products
{
    public record UpdateProductDto(
        decimal Price,
        string Name,
        decimal DiscountPrice,
        string Description,
        bool Gender,
        int CategoryId,
        int BrandId,
        ICollection<int> ColorIds,
        ICollection<int> SizeIds,
        IFormFile ? MainPhoto
        );
}
