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
    public class AdvertisementUserStatusProfile : Profile
    {
        public AdvertisementUserStatusProfile()
        {
            CreateMap<AdvertisementUserStatus, AdvertisementUserStatusListDto>().ReverseMap();
        }
    }
}
