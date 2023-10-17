using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Application.Services;

public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
{
    public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator,
        IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow) : 
        base(mapper, createDtoValidator, updateDtoValidator, uow)
    {
    }

}
