using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Application.ValidationRules.AppUserDtoValidators
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty")
                .Must(password => CheckLength(password)).WithMessage("Password length must be at least 8")
                .Must(password => ContainsDigit(password)).WithMessage("Password must contain at least one number");

        }

        private bool ContainsDigit(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return password.Any(char.IsDigit);
        }

        private bool CheckLength(string? password)
        {
            return password?.Length >= 8;
        }
    }
}
