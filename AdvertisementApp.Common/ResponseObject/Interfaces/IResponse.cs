using AdvertisementApp.Common.Enums;

namespace AdvertisementApp.Common.ResponseObject.Interfaces
{
    public interface IResponse
    {
        string? Message { get; set; }
        ResponseType ResponseType { get; set; }
    }
}
