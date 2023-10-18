using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Application.Services;
using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Application.ValidationRules.AppUserDtoValidators
{
    public class AppUserCreateDtoValidator : AbstractValidator<AppUserCreateDto>
    {
        //[Obsolete]
        public AppUserCreateDtoValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;


            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty").MinimumLength(8).WithMessage("The password must consist of at least 8 characters").Equal(x => x.ConfirmPassword).WithMessage("Password and Confirm Password do not match");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty").MinimumLength(5).WithMessage("The username must consist of at least 5 characters");

            RuleFor(x => new
            {
                x.Username,
                x.FirstName
            }).Must(x => CanNotFirstname(x.Username, x.FirstName)).WithMessage("username contains firstname!").When(x => x.Username != null && x.FirstName != null);


            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName cannot be empty");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber cannot be empty");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Gender cannot be empty");


            //RuleFor(x => x.Username).Must(username => UniqueUsername(username)).WithMessage("this username is already taken");

            RuleFor(x => x.FirstName).Must(firstname => Test(firstname)).WithMessage("Firstname xanış ola bilməz");
        }



        private bool Test(string? firstname)
        {
            if (string.IsNullOrEmpty(firstname))
                return true;

            return firstname != "Khanish";
        }

        private bool CanNotFirstname(string? username, string? firstName)
        {
            if (username == null || firstName == null)
            {
                return true;
            }
            return !username.Contains(firstName);
        }
    }
}
