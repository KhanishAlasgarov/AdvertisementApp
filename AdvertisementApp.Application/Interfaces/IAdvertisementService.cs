using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;

namespace AdvertisementApp.Application.Interfaces;

public interface IAdvertisementService : IService<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>
{
    Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync();
}
