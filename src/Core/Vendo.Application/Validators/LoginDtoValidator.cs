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
            RuleFor(x => x.Phone).NotEmpty()
             .WithMessage("Phone number is required");


            RuleFor(x => x.Password).NotEmpty()
                   .WithMessage("Password Required")
                 .MinimumLength(8)
                       .WithMessage("Password must exist min 8 symbols");
        }
      
    }
}
