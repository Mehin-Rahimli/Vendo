
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vendo.Application.Abstractions.Repositories;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.Files;
using Vendo.Application.DTOs.Products;
using Vendo.Domain.Entities;
using Vendo.Persistence.Implementations.Repostories;

namespace Vendo.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productrepo, IMapper mapper, 
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository, 
            IFileService fileService)
        {
            _productRepository = productrepo;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _fileService = fileService;
            _fileService = fileService;
        }

        public async Task<IEnumerable<ProductItemDto>> GetAll(int page, int take)
        {
            List<Product> products = await _productRepository
                    .GetAll(skip: (page - 1) * take, take: take)
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ProductItemDto>>(products);
        }
        public async Task<GetProductDto> GetByIdAsync(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id, "Category","Brand",
                "ProductColors.Color",
                "ProductSizes.Size"
                );
            if (product is null) throw new Exception("Not Found");
            return _mapper.Map<GetProductDto>(product);
        }
        public async Task CreateAsync(CreateProductDto  productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            if (product.Price >= 0 && product.Discount >= 0 && product.Discount <= 100&& product.Price>=product.DiscountPrice)
            {
                product.DiscountPrice = product.Price - (product.Price * (product.Discount / 100));
            }
            else
            {
                throw new ArgumentException("Wrong Price");
            }


            if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                throw new Exception("Category does not exists");
            if (!await _brandRepository.AnyAsync(c => c.Id == productDto.BrandId))
                throw new Exception("Brand does not exists");
            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(productDto.ColorIds);
            if (colorEntities.Count() != productDto.ColorIds.Distinct().Count())
                throw new Exception("Color id is wrong");
            
            
            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(productDto.SizeIds);
            if (sizeEntities.Count() != productDto.SizeIds.Distinct().Count())
                throw new Exception("Size id is wrong");
            FileDto main=await _fileService.AddImageAsync(productDto.mainPhoto);
            product.ProductImages = new List<ProductImage>
            {
                new ProductImage
                {
                    IsDeleted=false,    
                    ImageUrl=main.Url,

                    PublicId=main.PublicId,
                    IsPrimary=true
                }

            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, UpdateProductDto productDto)
        {
         
            Product product = await _productRepository.GetByIdAsync(id, "ProductColors", "ProductSizes","ProductImages");
           
            if (product.Price >= 0 && product.Discount >= 0 && product.Discount <= 100)
            {
                product.DiscountPrice = product.Price - (product.Price * (product.Discount / 100));
            }
            else
            {
                throw new ArgumentException("Wrong Price");
            }

            if (productDto.CategoryId != product.CategoryId)
                if (!await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId))
                    throw new Exception("Category does not exists");
            if (productDto.BrandId != product.BrandId)
                if (!await _brandRepository.AnyAsync(c => c.Id == productDto.BrandId))
                    throw new Exception("Brand does not exists");

            // product.ProductColors=product.ProductColors.Where(pc=>productDto.ColorIds.Contains(pc.ColorId)).ToList();

            ICollection<int> createItems = productDto.ColorIds
                .Where(cId => !product.ProductColors.Any(pc => pc.ColorId == cId)).ToList();
            var colorEntities = await _productRepository.GetManyToManyEntities<Color>(createItems);
            if (colorEntities.Count() != createItems.Distinct().Count())
                throw new Exception("One or more color id is wrong");
            


            ICollection<int> createItems2 = productDto.SizeIds
                .Where(cId => !product.ProductSizes.Any(ps => ps.SizeId == cId)).ToList();
            var sizeEntities = await _productRepository.GetManyToManyEntities<Size>(createItems2);
            if (sizeEntities.Count() != createItems2.Distinct().Count())
                throw new Exception("One or more size id is wrong");

            if (productDto.MainPhoto != null)
            { FileDto main=await _fileService.AddImageAsync(productDto.MainPhoto);
            
                var existedMain =product.ProductImages.FirstOrDefault(pi => pi.IsPrimary==true);
                await _fileService.DeleteImageAsync(existedMain.PublicId);
                product.ProductImages.Remove(existedMain);

                product.ProductImages.Add(new ProductImage
                {
                    IsDeleted = false,
                    ImageUrl = main.Url,
                    PublicId = main.PublicId,
                    IsPrimary = true
                });
            }

            _productRepository.Update(_mapper.Map(productDto, product));
            await _productRepository.SaveChangesAsync();

        }

    }
}
