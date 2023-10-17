using AdvertisementApp.Application.Extensions;
using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Domain.Common;
using AdvertisementApp.Dtos.Interfaces;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Application.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity
    {
        public Service(IMapper mapper, IValidator<CreateDto> createDtoValidator, IValidator<UpdateDto> updateDtoValidator, IUow uow)
        {
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            _uow = uow;
        }

        private protected IMapper _mapper { get; }
        private protected IValidator<CreateDto> _createDtoValidator { get; }
        private protected IValidator<UpdateDto> _updateDtoValidator { get; }
        private protected IUow _uow { get; }


        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);

            if (!result.IsValid)
            {
                return new Response<CreateDto>(dto, result.ConvertToCustomValidationError());
            }
            await _uow.GetRepository<T>().CreateAsync(_mapper.Map<T>(dto));
            return new Response<CreateDto>(ResponseType.Success, dto);
        }

        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {

            return new Response<List<ListDto>>(ResponseType.Success, _mapper.Map<List<ListDto>>(await _uow.GetRepository<T>().GetAllAsync()));
        }

        public async Task<IResponse<IDto>> GetByIdAsync<IDto>(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data == null)
                return new Response<IDto>(ResponseType.NotFound, $"Bu {id}-ə sahib data tapılmadı");

            return new Response<IDto>(ResponseType.Success, _mapper.Map<IDto>(data));
        }

        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().FindAsync(id);

            if (data == null)
                return new Response(ResponseType.NotFound, $"Bu {id}-ə sahib data tapılmadı");

            _uow.GetRepository<T>().Remove(_mapper.Map<T>(data));
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (!result.IsValid)
                return new Response<UpdateDto>(dto, result.ConvertToCustomValidationError());

            var unchanged = await _uow.GetRepository<T>().FindAsync(dto.Id);

            if (unchanged == null)
                return new Response<UpdateDto>(ResponseType.NotFound, $"Bu {dto.Id}-ə sahib data tapılmadı");

            _uow.GetRepository<T>().Update(_mapper.Map<T>(dto), unchanged);


            return new Response<UpdateDto>(ResponseType.Success, dto);

        }
    }
}
