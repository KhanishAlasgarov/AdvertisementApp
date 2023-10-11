using AdvertisementApp.Domain.Common;
using AdvertisementApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Contexts
{
    public class AdvertisementAppContext : DbContext
    {
        public AdvertisementAppContext(DbContextOptions<AdvertisementAppContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Advertisement> Advertisements{ get; set; }
        public DbSet<AdvertisementUser> AdvertisementUsers{ get; set; }
        public DbSet<AdvertisementUserStatus> AdvertisementUserStatuses { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Gender> Genders{ get; set; }
        public DbSet<MilitaryStatus> MilitaryStatuses{ get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }
    }
}
