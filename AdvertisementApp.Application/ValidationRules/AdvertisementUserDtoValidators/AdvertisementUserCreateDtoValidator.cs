using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Application.ValidationRules.AdvertisementUserDtoValidators
{
    public class AdvertisementUserCreateDtoValidator : AbstractValidator<AdvertisementUserCreateDto>
    {
        public AdvertisementUserCreateDtoValidator()
        {
            RuleFor(x => new { x.MilitaryStatusId, x.EndDate }).Must(x => CheckEndDate(x.MilitaryStatusId, x.EndDate))
                .WithMessage("Postponement date cannot be left blank.");

            //RuleFor(x => x.EndDate).NotEmpty().When(x => x.MilitaryStatusId == (int)MilitaryStatusType.Deferred).WithMessage("Postponement date cannot be left blank."); ;

            
        }

        private bool CheckEndDate(int id, DateTime? enddate)
        {
            if (id == (int)MilitaryStatusType.Deferred && enddate == null)
            {
                return false;
            }
            return true;
        }
    }
}
