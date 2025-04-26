using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.DTOs.Products;

namespace Vendo.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetAll(int page, int take);
        Task<GetProductDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(int id, UpdateProductDto productDto);

    }
}
