using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos;

public class AppRoleUpdateDto:IDto
{
    public int Id { get; set; }
    public string? Definition { get; set; }
}
