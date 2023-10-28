using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AdvertisementApp.Dtos;

public class AdvertisementUserCreateDto : IDto
{
    public int AdvertisementId { get; set; }
    public int AppUserId { get; set; }
    public int AdvertisementUserStatusId { get; set; } = (int)AdvertisementAppUserStatusType.Applied;
    public int MilitaryStatusId { get; set; }
    public DateTime? EndDate { get; set; }
    public int WorkExperience { get; set; }
    public string? CvPath { get; set; }


}
