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
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3)
                .Matches(@"^[A-Za-z]*$");

            RuleFor(r => r.Surname)
               .NotEmpty()
               .MaximumLength(50)
               .MinimumLength(3)
               .Matches(@"^[A-Za-z]*$");


            RuleFor(r => r.UserName)
               .NotEmpty()
               .MaximumLength(256)
               .MinimumLength(4)
               .Matches(@"^[A-Za-z0-9-._@+]*$");

            RuleFor(r => r.Email)
               .NotEmpty()
               .MaximumLength(256)
               .MinimumLength(6)
               .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            RuleFor(r => r.Password)
               .NotEmpty()
               .MaximumLength(100)
               .MinimumLength(8);


        }
    }
}
