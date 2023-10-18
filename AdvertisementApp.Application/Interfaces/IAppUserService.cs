using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;

namespace AdvertisementApp.Application.Interfaces;

public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
{
    bool FindByName(string? username);
}
