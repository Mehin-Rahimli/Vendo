using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vendo.Application;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.Abstractions.Services;
using Vendo.Domain.Entities;

namespace Vendo.Persistence.Implementations.Services
{
    internal class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }


        public async Task CreateAsync(CreateSizeDto sizeDto)
        {

            if (await _sizeRepository.AnyAsync(s => s.Name == sizeDto.Name)) throw new Exception("Already exists");

            Size size = _mapper.Map<Size>(sizeDto);

            size.CreatedAt = DateTime.Now;
            size.ModifiedAt = DateTime.Now;

            await _sizeRepository.AddAsync(size);
            await _sizeRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           Size size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new Exception("Not found");
            _sizeRepository.Delete(size);
            await _sizeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<SizeItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Size> sizes = await _sizeRepository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<SizeItemDto>>(sizes);
        }

        public async Task<GetSizeDto> GetByIdAsync(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) return null;
            GetSizeDto sizeDto = _mapper.Map<GetSizeDto>(size);
            return sizeDto;
        }




        public async Task UpdateAsync(int id, UpdateSizeDto sizeDto)
        {
         Size size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new Exception("Not found");
            if (await _sizeRepository.AnyAsync(s => s.Name == sizeDto.Name && s.Id != id)) 
                throw new Exception("Already exists");

            size = _mapper.Map<Size>(sizeDto);

            size.Name = sizeDto.Name;
            size.ModifiedAt = DateTime.Now;

            _sizeRepository.Update(size);
            await _sizeRepository.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            Size size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) throw new Exception("Not found");
            size.IsDeleted = true;
            _sizeRepository.Update(size);
            await _sizeRepository.SaveChangesAsync();
        }


    }
}

