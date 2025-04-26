using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.Abstractions.Services;

using Vendo.Application;
using Vendo.Domain.Entities;
using Vendo.Persistence.Implementations.Repostories;


namespace Vendo.Persistence.Implementations.Services
{
    internal class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository colorRepository,IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }


        public async Task CreateAsync(CreateColorDto colorDto)
        {

            if (await _colorRepository.AnyAsync(c => c.Name == colorDto.Name)) throw new Exception("Already exists");

            Color color = _mapper.Map<Color>(colorDto);

            color.CreatedAt = DateTime.Now;
            color.ModifiedAt = DateTime.Now;

            await _colorRepository.AddAsync(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            _colorRepository.Delete(color);
            await _colorRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ColorItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Color> colors = await _colorRepository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<ColorItemDto>>(colors);
        }

        public async Task<GetColorDto> GetByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) return null;
            GetColorDto colorDto = _mapper.Map<GetColorDto>(color);               
            return colorDto;
        }




        public async Task UpdateAsync(int id, UpdateColorDto colorDto)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            if (await _colorRepository.AnyAsync(c => c.Name == colorDto.Name && c.Id != id)) throw new Exception("Already exists");

            color = _mapper.Map<Color>(colorDto);
            color.Name = colorDto.Name;
            color.ModifiedAt = DateTime.Now;

            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not found");
            color.IsDeleted = true;
            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }

    }
}
