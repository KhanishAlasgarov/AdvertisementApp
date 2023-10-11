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
    internal class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasOne(x => x.AppUser).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.AppRoleId);

            builder.HasIndex(x => new
            {
                x.AppUserId,
                x.AppRoleId
            }).IsUnique();
        }
    }
}
