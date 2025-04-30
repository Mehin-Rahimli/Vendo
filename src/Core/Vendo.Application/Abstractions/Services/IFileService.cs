using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vendo.Application.DTOs.Files;

namespace Vendo.Application.Abstractions.Services
{
  public interface IFileService
    {
        public Task<FileDto> AddImageAsync(IFormFile file);
        public Task DeleteImageAsync(string publicId);

    }
}
