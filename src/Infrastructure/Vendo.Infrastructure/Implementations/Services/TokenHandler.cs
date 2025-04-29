using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.Abstractions.Services;
using Vendo.Application.DTOs.Tokens;
using Vendo.Domain.Entities;

namespace Vendo.Infrastructure.Implementations.Services
{
    internal class TokenHandler:ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenHandleDto CreateToken(AppUser user, IEnumerable<Claim> claims, int minutes)
        {


            SymmetricSecurityKey securitykey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_configuration["JWT:secretkey"]));

            SigningCredentials credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securitytoken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.Now.AddMinutes(minutes),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //string token = handler.WriteToken(securitytoken);

            TokenHandleDto dto = new TokenHandleDto(handler.WriteToken(securitytoken), securitytoken.ValidTo, user.UserName, CreateRefreshToken(), securitytoken.ValidTo.AddMinutes(minutes / 4));
            return dto;
        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
