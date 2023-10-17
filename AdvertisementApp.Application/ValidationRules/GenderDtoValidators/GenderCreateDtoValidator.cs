using AdvertisementApp.Dtos;
using FluentValidation;

namespace AdvertisementApp.Application.ValidationRules.GenderDtoValidators;

public class GenderCreateDtoValidator:AbstractValidator<GenderCreateDto>
{
    public GenderCreateDtoValidator()
    {
        RuleFor(x => x.Definition).NotEmpty();
    }
}
