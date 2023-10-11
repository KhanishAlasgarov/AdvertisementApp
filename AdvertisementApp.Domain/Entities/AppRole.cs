using AdvertisementApp.Domain.Common;

namespace AdvertisementApp.Domain.Entities;

public class AppRole:BaseEntity
{
    public string? Definition { get; set; }
    public List<AppUserRole>? AppUserRoles { get; set; }
}
