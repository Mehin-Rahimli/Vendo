
using AutoMapper;
using Vendo.Application.DTOs.AppUsers;
using Vendo.Domain.Entities;

namespace Vendo.Application.MappingProfiles
{
    internal class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Phone}@dummy.com")); ;
        }
      
    }
}
