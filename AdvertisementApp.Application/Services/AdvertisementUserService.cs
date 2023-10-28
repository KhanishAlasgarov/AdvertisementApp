using AdvertisementApp.Application.Extensions;
using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementApp.Application.Services;

public class AdvertisementUserService : Service<AdvertisementUserCreateDto, AdvertisementUserUpdateDto, AdvertisementUserListDto, AdvertisementUser>, IAdvertisementUserService
{
    public AdvertisementUserService(IMapper mapper, IValidator<AdvertisementUserCreateDto> createDtoValidator,
                                    IValidator<AdvertisementUserUpdateDto> updateDtoValidator, IUow uow)
                                    : base(mapper, createDtoValidator, updateDtoValidator, uow)
    {

    }
    public async Task<IResponse<AdvertisementUserCreateDto>> CreateWithCvAsync(AdvertisementUserCreateDto dto)
    {
        var validationResult = _createDtoValidator.Validate(dto);

        if (!validationResult.IsValid)
        {
            return new Response<AdvertisementUserCreateDto>(dto, validationResult.ConvertToCustomValidationError());
        }

        var control = await _uow.GetRepository<AdvertisementUser>()
            .GetByFilterAsync(x => x.AppUserId == dto.AppUserId && dto.AdvertisementId == x.AdvertisementId);

        if (control.Count >= 1)
        {
            return new Response<AdvertisementUserCreateDto>(ResponseType.Error, $"You applied to the previously applied posting.");
        }

        await _uow.GetRepository<AdvertisementUser>().CreateAsync(_mapper.Map<AdvertisementUser>(dto));
        await _uow.SaveChangesAsync();
        return new Response<AdvertisementUserCreateDto>(ResponseType.Success, dto);
    }
    public async Task<IResponse<List<AdvertisementUserListDto>>> GetApplicationsAsync(AdvertisementAppUserStatusType advertisementAppUserStatusType)
    {

        var data = _mapper.Map<List<AdvertisementUserListDto>>(await _uow.GetRepository<AdvertisementUser>().GetQuery()
             .Include(x => x.AppUser).ThenInclude(x => x.Gender).Include(x => x.Advertisement)
             .Include(x => x.AdvertisementUserStatus).Include(x => x.MilitaryStatus)
             .Where(x => x.AdvertisementUserStatusId == (int)advertisementAppUserStatusType).ToListAsync());

        if (data.Count <= 0)
        {
            return new Response<List<AdvertisementUserListDto>>(ResponseType.NotFound, $"Tapılmadı");
        }

        return new Response<List<AdvertisementUserListDto>>(ResponseType.Success, data);
    }

    public async Task<IResponse> SetStatusAsync(int advertisementUserId, AdvertisementAppUserStatusType type)
    {
        var unchanged = await _uow.GetRepository<AdvertisementUser>().FindAsync(advertisementUserId);
        if (unchanged == null)
        {
            return new Response(ResponseType.NotFound, $"Doğru id göndərin zəhmət olmasa");
        }
        var changed = await _uow.GetRepository<AdvertisementUser>().FindAsync(advertisementUserId);
        changed.AdvertisementUserStatusId = (int)type;
        _uow.GetRepository<AdvertisementUser>().Update(changed, unchanged);
        await _uow.SaveChangesAsync();
        return new Response(ResponseType.Success);


    }

}
