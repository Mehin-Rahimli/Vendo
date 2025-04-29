using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendo.Application.DTOs.AppUsers;

namespace Vendo.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3)
                .Matches(@"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$");


            RuleFor(r => r.Phone)
               .NotEmpty()
               .MaximumLength(20)
               .MinimumLength(4);

          
            RuleFor(r => r.Password)
               .NotEmpty()
               .MaximumLength(100)
               .MinimumLength(8);


        }
    }
}
