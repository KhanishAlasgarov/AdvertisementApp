using AdvertisementApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Domain.Entities
{
    public class AdvertisementUser : BaseEntity
    {
        public Advertisement? Advertisement { get; set; }
        public int AdvertisementId { get; set; }
        public AppUser? AppUser { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementUserStatusId { get; set; }
        public AdvertisementUserStatus? AdvertisementUserStatus { get; set; }
        public int MilitaryStatusId { get; set; }
        public MilitaryStatus? MilitaryStatus { get; set; }
        public DateTime? EndDate { get; set; }
        public int WorkExperience { get; set; }
        public string? CvPath { get; set; }
    }
}
