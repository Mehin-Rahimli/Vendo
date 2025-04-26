

namespace Vendo.Application.DTOs.Products
{
   public record CreateProductDto(decimal Price,string Name,decimal DiscountPrice,decimal Discount,string Description,bool Gender,
         ICollection<int>ColorIds,
         ICollection<int> SizeIds,
     
        int CategoryId,
        int BrandId


       );
    
}
