using AdvertisementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Configurations
{
    internal class AdvertisementUserStatusConfiguration : IEntityTypeConfiguration<AdvertisementUserStatus>
    {
        public void Configure(EntityTypeBuilder<AdvertisementUserStatus> builder)
        {
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(300);

            builder.HasData(new AdvertisementUserStatus[]
            {
                new AdvertisementUserStatus{
                Id=1,
                Definition= "Done"
                },
                new AdvertisementUserStatus{
                Id=2,
                Definition= "Deferred"
                },
                new AdvertisementUserStatus{
                Id=3,
                Definition= "Exempt"
                }
            });
        }
    }
}
