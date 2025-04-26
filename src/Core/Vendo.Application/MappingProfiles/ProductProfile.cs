using AutoMapper;
using Vendo.Application.DTOs.Products;
using Vendo.Domain.Entities;


namespace Vendo.Application.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductItemDto>();
            CreateMap<Product, GetProductDto>()
                .ForCtorParam(nameof(GetProductDto.Colors),
                opt => opt.MapFrom(
                    p => p.ProductColors.Select(pc => new ColorItemDto(pc.ColorId, pc.Color.Name)).ToList())
                ).ForCtorParam(nameof(GetProductDto.Sizes),
               opt => opt.MapFrom(
                   p => p.ProductSizes.Select(ps => new SizeItemDto(ps.SizeId, ps.Size.Name)).ToList())
               );

            CreateMap<CreateProductDto, Product>()
               .ForMember(p => p.ProductColors,
               opt => opt.MapFrom(pDto => pDto.ColorIds.Select(ci => new ProductColor { ColorId = ci }))
               )
               .ForMember(p => p.ProductSizes,
               opt => opt.MapFrom(pDto => pDto.SizeIds.Select(si => new ProductSize { SizeId = si }))
               );


            CreateMap<UpdateProductDto, Product>()
               .ForMember(
                p => p.Id,
                opt => opt.Ignore())
                .ForMember(
                p => p.ProductColors,
                opt => opt.MapFrom(pDto => pDto.ColorIds.Select(ci => new ProductColor { ColorId = ci }))

                )
                .ForMember(
                p => p.ProductSizes,
                opt => opt.MapFrom(pDto => pDto.SizeIds.Select(ci => new ProductSize { SizeId = ci }))
                );
              
        }
    }
}
