using AdvertisementApp.Common.ResponseObject;
using FluentValidation.Results;

namespace AdvertisementApp.Application.Extensions;

public static class ValidationResultExtension
{
    public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(x => new CustomValidationError
        {
            ErrorMessage = x.ErrorMessage,
            PropertyName = x.PropertyName
        }).ToList();

    }
}
