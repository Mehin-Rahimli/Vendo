using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendo.Application.DTOs.AppUsers
{
    public record LoginDto(string UserNameOrEmail, string Password);
}
