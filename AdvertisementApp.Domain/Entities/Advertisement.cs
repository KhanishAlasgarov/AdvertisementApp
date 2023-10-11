using AdvertisementApp.Domain.Common;

namespace AdvertisementApp.Domain.Entities;

public class Advertisement:BaseEntity
{
    public string? Title { get; set; }
    public bool Status { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<AdvertisementUser>? AdvertisementUsers{ get; set; }
}
