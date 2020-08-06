using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.System
{
   public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password need to be greater than 8 characters.");
        }
    }
}
