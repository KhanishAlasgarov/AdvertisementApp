using AdvertisementApp.Domain.Common;

namespace AdvertisementApp.Domain.Entities;

public class AppUserRole:BaseEntity
{
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public int AppRoleId { get; set; }
    public AppRole? AppRole { get; set; }
}
