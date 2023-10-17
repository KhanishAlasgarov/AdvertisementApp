using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;

namespace AdvertisementApp.Application.Mappings.AutoMapper;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
        CreateMap<AppUser, AppUserCreateDto>().ReverseMap();
        CreateMap<AppUser, AppUserListDto>().ReverseMap();
        CreateMap<AppUser, AppUserUpdateDto>().ReverseMap();
    }
}
