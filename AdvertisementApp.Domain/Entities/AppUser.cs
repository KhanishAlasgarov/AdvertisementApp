using AdvertisementApp.Domain.Common;

namespace AdvertisementApp.Domain.Entities;

public class AppUser : BaseEntity
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public int GenderId { get; set; }
    public List<AppUserRole>? AppUserRole { get; set; }
    public List<AdvertisementUser>? AdvertisementUser { get; set; }


}
