using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.DTOs.Tokens;
using Vendo.Domain.Entities;

namespace Vendo.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenHandleDto CreateToken(AppUser user, IEnumerable<Claim> claims, int minutes);
        string CreateRefreshToken();
    }
}
