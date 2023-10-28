using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos;

public class AdvertisementUserListDto : IDto
{
    public int Id { get; set; }
    public AdvertisementListDto Advertisement { get; set; }
    public AppUserListDto AppUser { get; set; }
    public AdvertisementUserStatusListDto AdvertisementUserStatus { get; set; }
    public MilitaryStatusListDto MilitaryStatus { get; set; }
    public DateTime? EndDate { get; set; }
    public int WorkExperience { get; set; }
    public string? CvPath { get; set; }
}
