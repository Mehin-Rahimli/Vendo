

namespace Vendo.Application.Abstractions.Services
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeItemDto>> GetAllAsync(int page, int take);
        Task<GetSizeDto> GetByIdAsync(int id);
        Task CreateAsync(CreateSizeDto sizeDto);
        Task UpdateAsync(int id, UpdateSizeDto sizeDto);
        Task DeleteAsync(int id);
    }
}
