using AdvertisementApp.Common.Enums;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Application.Interfaces
{
    public interface IAdvertisementUserService : IService<AdvertisementUserCreateDto, AdvertisementUserUpdateDto, AdvertisementUserListDto, AdvertisementUser>
    {
        Task<IResponse<AdvertisementUserCreateDto>> CreateWithCvAsync(AdvertisementUserCreateDto dto);
        Task<IResponse<List<AdvertisementUserListDto>>> GetApplicationsAsync(AdvertisementAppUserStatusType advertisementAppUserStatusType);
        Task<IResponse> SetStatusAsync(int advertisementUserId, AdvertisementAppUserStatusType type);
    }
}
