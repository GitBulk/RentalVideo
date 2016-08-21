using FluentValidation;
using RentalVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalVideo.Infrastructure.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage("Invalid username");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Invalid pasword");
        }
    }
}