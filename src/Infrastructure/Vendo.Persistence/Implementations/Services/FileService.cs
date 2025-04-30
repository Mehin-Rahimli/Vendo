
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.Files;
using Vendo.Infrastructure.OptionClasses;
namespace Vendo.Persistence.Implementations.Services
{
    internal class FileService : IFileService
    {
        public readonly Cloudinary _cloudinary;
        public async Task <FileDto> AddImageAsync(IFormFile file)
        {
            string fileName = Guid.NewGuid+ file.FileName;
            ImageUploadResult result = new ImageUploadResult();
            using (var stream = file.OpenReadStream())
            {
                ImageUploadParams imageParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                    Transformation = new Transformation().Height(800).Width(500)
                  ,


                };
               result= await _cloudinary.UploadAsync(imageParams);
            }
            return new FileDto(fileName,result.SecureUrl.ToString(),result.PublicId);
        }
        
           public FileService(IOptions<CloudSettings> cloudConfig)
        {
           Account account= new Account(cloudConfig.Value.Name,
               cloudConfig.Value.Key,
               cloudConfig.Value.Secret);

          _cloudinary=  new Cloudinary(account);
            
        }
        
        public async Task DeleteImageAsync(string publicId)
        {
            DeletionParams deleteionParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteionParams);
        }
    }

}
