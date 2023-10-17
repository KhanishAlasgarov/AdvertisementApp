using AdvertisementApp.Dtos;
using FluentValidation;

namespace AdvertisementApp.Application.ValidationRules.AppUserDtoValidators;

public class AppUserUpdateDtoValidator:AbstractValidator<AppUserUpdateDto>
{
    public AppUserUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.GenderId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
    }
}
