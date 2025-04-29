using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendo.Application.DTOs.Tokens
{
    public record TokenHandleDto
    (
        string Token,
        DateTime Expires,
        string UserName,
        string RefreshToken,
        DateTime RefreshExpireTime
    );
}
