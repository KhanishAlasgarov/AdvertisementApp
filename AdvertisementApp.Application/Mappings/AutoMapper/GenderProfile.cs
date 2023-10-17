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
    public class GenderProfile:Profile
    {
        public GenderProfile()
        {
            CreateMap<Gender, GenderCreateDto>().ReverseMap();
            CreateMap<Gender, GenderUpdateDto>().ReverseMap();
            CreateMap<Gender, GenderListDto>().ReverseMap();
        }
    }
}
