﻿using AdvertisementApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Domain.Entities
{
    public class MilitaryStatus:BaseEntity
    {
        public string? Definition { get; set; }
        public List<AdvertisementUser>? AdvertisementUsers { get; set; }
    }
}
