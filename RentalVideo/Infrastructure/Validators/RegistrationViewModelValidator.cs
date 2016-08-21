using FluentValidation;
using RentalVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalVideo.Infrastructure.Validators
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
            RuleFor(r => r.UserName).NotEmpty().WithMessage("Invalid username");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Invalid password");
        }
    }
}