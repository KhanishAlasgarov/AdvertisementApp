using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Application.Services;

public class AdvertisementService : Service<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
{
    public AdvertisementService(IMapper mapper, IValidator<AdvertisementCreateDto> createDtoValidator, IValidator<AdvertisementUpdateDto> updateDtoValidator, IUow uow) : base(mapper, createDtoValidator, updateDtoValidator, uow)
    {

    }

    public async Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync()
    {
        var data = await _uow.GetRepository<Advertisement>().GetAllAsync(x=>x.CreatedDate,x=>x.Status);
        return new Response<List<AdvertisementListDto>>(ResponseType.Success, _mapper.Map<List<AdvertisementListDto>>(data));
    }
}
