using Domain.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Auth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {

        public LoginCommandValidator()
        {
            RuleFor(v => v.Password).NotEmpty().NotNull().WithMessage("Password is required");
            RuleFor(v => v.UserName).NotEmpty().NotNull().WithMessage("Email is required") ;

        }

        
    }
}
