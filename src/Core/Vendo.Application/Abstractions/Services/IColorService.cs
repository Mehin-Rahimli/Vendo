

namespace Vendo.Application.Abstractions.Services
{
    public interface IColorService
    {
       
            Task<IEnumerable<ColorItemDto>> GetAllAsync(int page, int take);
            Task<GetColorDto> GetByIdAsync(int id);
            Task CreateAsync(CreateColorDto colorDto);
            Task UpdateAsync(int id, UpdateColorDto colorDto);
            Task DeleteAsync(int id);
        }
    }

