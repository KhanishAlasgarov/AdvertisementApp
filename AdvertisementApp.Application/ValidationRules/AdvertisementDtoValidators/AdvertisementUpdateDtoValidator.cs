using AdvertisementApp.Dtos;
using FluentValidation;

namespace AdvertisementApp.Application.ValidationRules.AdvertisementDtoValidators;

public class AdvertisementUpdateDtoValidator : AbstractValidator<AdvertisementUpdateDto>
{
    public AdvertisementUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
