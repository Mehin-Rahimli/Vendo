using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.AppUsers;
using Vendo.Application.DTOs.Tokens;
using Vendo.Domain.Entities;
using Vendo.Domain.Enums;

namespace Vendo.Persistence.Implementations.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<TokenHandleDto> LoginAsync(LoginDto userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == userDto.Phone);
            if (user is null) throw new Exception("Name Or phone is incorrect!");

            var result = await _userManager.CheckPasswordAsync(user, userDto.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception("Name Or phone is incorrect!");
            }

            ICollection<Claim> claims = await _createClaims(user);

            TokenHandleDto tokenDto = _tokenHandler.CreateToken(user, claims, 1);

            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshToxenExpiredAt = tokenDto.RefreshExpireTime;
            await _userManager.UpdateAsync(user);

            return tokenDto;

        }

        private async Task<ICollection<Claim>> _createClaims(AppUser user)
        {
            ICollection<Claim> claims = new List<Claim> {
             new Claim(ClaimTypes.NameIdentifier, user.Id),
             new Claim(ClaimTypes.Name, user.FullName),
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<TokenHandleDto> LoginByRefreshToken(string refresh)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refresh);
            if (user is null) throw new Exception("Not found");
            if (user.RefreshToxenExpiredAt < DateTime.UtcNow) throw new Exception("Expired");

            TokenHandleDto tokenDto = _tokenHandler.CreateToken(user, await _createClaims(user), 60);


            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshToxenExpiredAt = tokenDto.RefreshExpireTime;
            await _userManager.UpdateAsync(user);

            return tokenDto;
        }

        public async Task RegisterAsync(RegisterDto userDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == userDto.Phone))
                throw new Exception("Phone number is already exist");
            var user = _mapper.Map<AppUser>(userDto);

            var result = await _userManager.CreateAsync(user, userDto.Password);

            StringBuilder message = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    message.AppendLine(error.Description);
                }
                throw new Exception(message.ToString());
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
        }
    }
}
