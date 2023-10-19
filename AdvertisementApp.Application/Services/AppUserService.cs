using AdvertisementApp.Application.Extensions;
using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementApp.Application.Services;

public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
{
    public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator,
        IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow) :
        base(mapper, createDtoValidator, updateDtoValidator, uow)
    {
    }

    public async Task<IResponse<AppUserCreateDto>> CreateAsync(AppUserCreateDto dto, int roleid)
    {
        var validateResult = await _createDtoValidator.ValidateAsync(dto);

        if (!validateResult.IsValid)
        {
            return new Response<AppUserCreateDto>(dto, validateResult.ConvertToCustomValidationError());
        }
        var user = _mapper.Map<AppUser>(dto);
        await _uow.GetRepository<AppUser>().CreateAsync(user);
        await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
        {
            AppRoleId = roleid,
            AppUser = user
        });

        await _uow.SaveChangesAsync();

        return new Response<AppUserCreateDto>(ResponseType.Success, dto);
    }
}
