using AdvertisementApp.Domain.Common;

namespace AdvertisementApp.Domain.Entities;

public class AppUser : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public int GenderId { get; set; }
    public List<AppUserRole>? AppUserRoles { get; set; }
    public List<AdvertisementUser>? AdvertisementUsers { get; set; }


}
