

using Microsoft.AspNetCore.Http;
using Vendo.Domain.Enums;

namespace Vendo.Application.DTOs.Products
{
   public record CreateProductDto(decimal Price,string Name,decimal DiscountPrice,decimal Discount,string Description,Gender Gender,AgeGroup AgeGroup,
         ICollection<int>ColorIds,
         ICollection<int> SizeIds,
     
        int CategoryId,
        int BrandId,

       IFormFile mainPhoto
       
       );
    
}
