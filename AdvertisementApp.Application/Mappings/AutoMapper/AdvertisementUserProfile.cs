using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Application.Mappings.AutoMapper
{
    public class AdvertisementUserProfile : Profile
    {
        public AdvertisementUserProfile()
        {
            CreateMap<AdvertisementUser, AdvertisementUserCreateDto>().ReverseMap();
            CreateMap<AdvertisementUser, AdvertisementUserListDto>().ReverseMap();
            CreateMap<AdvertisementUser, AdvertisementUserUpdateDto>().ReverseMap();
        }
    }
}
