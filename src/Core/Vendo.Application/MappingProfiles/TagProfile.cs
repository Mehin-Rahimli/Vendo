
using AutoMapper;
using Vendo.Domain.Entities;

namespace Vendo.Application.MappingProfiles
{
    internal class BrandProfile :Profile
    {
        public BrandProfile()
        {

            CreateMap<Brand, GetBrandDto>().ReverseMap();
            CreateMap<Brand, BrandItemDto>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<UpdateBrandDto, Brand>();
        }
    }
}
