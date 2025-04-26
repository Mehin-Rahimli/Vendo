using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.Abstractions.Services;
using Vendo.Application;
using Vendo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Vendo.Persistence.Implementations.Services
{
    internal class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            if (await _brandRepository.AnyAsync(c => c.Name == brandDto.Name)) throw new Exception("Already exists");

            var brand = _mapper.Map<Brand>(brandDto);

          brand.CreatedAt = DateTime.Now;
            brand.ModifiedAt = DateTime.Now;

            await _brandRepository.AddAsync(brand);
            await _brandRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
           var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) throw new Exception("Not found");
            _brandRepository.Delete(brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Brand> brands = await _brandRepository
                 .GetAll(skip: (page - 1) * take, take: take)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<BrandItemDto>>(brands);
        }

        public async Task<GetBrandDto> GetByIdAsync(int id)
        {

         var brand = await _brandRepository.GetByIdAsync(id, nameof(Brand.Products));


            if (brand == null) return null;



           var brandDto = _mapper.Map<GetBrandDto>(brand);

            return brandDto;
        }




        public async Task UpdateAsync(int id, UpdateBrandDto brandDto)
        {
     var brand = await _brandRepository.GetByIdAsync(id);
            if(brand == null) throw new Exception("Not found");
            if (await _brandRepository.AnyAsync(c => c.Name == brandDto.Name && c.Id != id)) throw new Exception("Already exists");

           brand= _mapper.Map<Brand>(brandDto);
            brand.Id = id;

           brand.Name = brandDto.Name;
            brand.ModifiedAt = DateTime.Now;

            _brandRepository.Update(brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
           var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) throw new Exception("Not found");
            brand.IsDeleted = true;
            _brandRepository.Update(brand);
            await _brandRepository.SaveChangesAsync();
        }




    
}

}
