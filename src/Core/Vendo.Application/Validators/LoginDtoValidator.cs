using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.DTOs.AppUsers;

namespace Vendo.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail).NotEmpty()
             .WithMessage("UserName or Email Required")
           .MaximumLength(256)
                 .WithMessage("UserName or Email must exist max 256 symbols");


            RuleFor(x => x.Password).NotEmpty()
                   .WithMessage("Password Required")
                 .MinimumLength(8)
                       .WithMessage("Password must exist min 8 symbols");
        }
      
    }
}
