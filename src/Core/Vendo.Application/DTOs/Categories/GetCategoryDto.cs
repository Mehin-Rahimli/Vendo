using Vendo.Application.DTOs.Products;


namespace Vendo.Application
{ 
    public record GetCategoryDto(int Id,string Name,ICollection<ProductItemDto>Products);
}
