using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos;

public class AppUserCreateDto : IDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? PhoneNumber { get; set; }
    public int GenderId { get; set; }
}
