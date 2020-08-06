using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.System
{
   public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required")
                .MaximumLength(200).WithMessage("Full name has been exceed 200 characters limited.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address has been exceed 200 characters limited.");


            RuleFor(x => x.Email).NotNull().WithMessage("Email is required")
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format is not correct");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(3).WithMessage("Password need to be greater than 3 characters.");

           
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.ConfirmPassword), "Confirmation password is not correct");
                }
            });
        }
    }
}
