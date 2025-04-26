using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Vendo.Application.MappingProfiles
{
    internal class Size:Profile
    {
        public Size()
        {

            CreateMap<Size, GetSizeDto>().ReverseMap();
            CreateMap<Size, SizeItemDto>();
            CreateMap<CreateSizeDto, Size>();
            CreateMap<UpdateSizeDto, Size>();
        }
    }
}
