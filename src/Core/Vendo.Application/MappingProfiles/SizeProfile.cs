
using AutoMapper;
using Vendo.Domain.Entities;

namespace Vendo.Application.MappingProfiles
{
    internal class SizeProfile:Profile
    {
        public SizeProfile()
        {

            CreateMap<Size, GetSizeDto>().ReverseMap();
            CreateMap<Size, SizeItemDto>();
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>().ForMember(s => s.Id, opt => opt.Ignore());
        }
    }
}
