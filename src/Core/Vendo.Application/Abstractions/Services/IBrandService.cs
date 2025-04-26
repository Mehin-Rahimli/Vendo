
namespace Vendo.Application.Abstractions.Services
{
   
        public interface IBrandService
        {
            Task<IEnumerable<BrandItemDto>> GetAllAsync(int page, int take);
            Task<GetBrandDto> GetByIdAsync(int id);
            Task CreateAsync(CreateBrandDto brandDto);
            Task UpdateAsync(int id, UpdateBrandDto brandDto);
            Task DeleteAsync(int id);
        }
  
}
