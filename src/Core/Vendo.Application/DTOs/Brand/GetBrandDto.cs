using Vendo.Application.DTOs.Products;


namespace Vendo.Application
{ 
    public record GetBrandDto(int Id,string Name,ICollection<ProductItemDto>Products);
}
