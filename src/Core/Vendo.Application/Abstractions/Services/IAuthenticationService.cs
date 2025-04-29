using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.DTOs.AppUsers;
using Vendo.Application.DTOs.Tokens;

namespace Vendo.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto userDto);
        Task<TokenHandleDto> LoginAsync(LoginDto userDto);
        Task<TokenHandleDto> LoginByRefreshToken(string refresh);
    }
}
