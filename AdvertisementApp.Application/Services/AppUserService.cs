using AdvertisementApp.Application.Interfaces;
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

    public bool FindByName(string? username)
    {
        if (username == null)
            return false;

        var users = _uow.GetRepository<AppUser>().GetQuery();

        var user = users.FirstOrDefault(x => x.Username == username);

        if (user == null)
            return false;

        return true;
    }
}
