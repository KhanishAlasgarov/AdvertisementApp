using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;

namespace AdvertisementApp.Application.Interfaces;

public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
{
    Task<IResponse<AppUserCreateDto>> CreateAsync(AppUserCreateDto dto, int roleid);
}
