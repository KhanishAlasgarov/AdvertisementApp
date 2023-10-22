using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;

namespace AdvertisementApp.Application.Interfaces;

public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
{
    Task<IResponse<AppUserCreateDto>> CreateAsync(AppUserCreateDto dto, int roleid);
    Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto);
    Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int? id);
}
