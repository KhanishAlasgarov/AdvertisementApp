using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;

namespace AdvertisementApp.Application.Mappings.AutoMapper;

public class MilitaryStatusProfile : Profile
{
    public MilitaryStatusProfile()
    {
        CreateMap<MilitaryStatusListDto, MilitaryStatus>().ReverseMap();
    }
}
