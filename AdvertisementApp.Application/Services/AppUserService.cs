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
    public IValidator<AppUserLoginDto> _loginDtoValidator { get; }
    public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator,
        IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow, IValidator<AppUserLoginDto> loginDtoValidator) :
        base(mapper, createDtoValidator, updateDtoValidator, uow)
    {
        this._loginDtoValidator = loginDtoValidator;
    }

    public async Task<IResponse<AppUserCreateDto>> CreateAsync(AppUserCreateDto dto, int roleid)
    {
        var validationResult = await _createDtoValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new Response<AppUserCreateDto>(dto, validationResult.ConvertToCustomValidationError());
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
    public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
    {
        var validationResult = await _loginDtoValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new Response<AppUserListDto>(ResponseType.ValidationError, "Username or Password incorrect");
        }

        //var user = await _uow.GetRepository<AppUser>().GetByFilterAsync(x => x.Username == dto.Username && x.Password == dto.Password);
        var users = _uow.GetRepository<AppUser>().GetQuery();
        var user = await users.FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);
        if (user != null)
        {
            var appUserListDto = _mapper.Map<AppUserListDto>(user);
            return new Response<AppUserListDto>(ResponseType.Success, appUserListDto);
        }
        return new Response<AppUserListDto>(ResponseType.NotFound, "Username or Password incorrect");

    }

    public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int? id)
    {
        var roles = await _uow.GetRepository<AppRole>().GetAllAsync(x => x.AppUserRoles.Any(appUserRole => appUserRole.AppUserId == id));

        if (roles == null)
            return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "Role Not Found");

        return new Response<List<AppRoleListDto>>(ResponseType.Success, _mapper.Map<List<AppRoleListDto>>(roles));
    }
}
